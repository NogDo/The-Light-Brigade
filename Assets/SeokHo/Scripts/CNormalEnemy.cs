using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.AI;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    // ��� ��� AI ������Ʈ ���� ����
    private NavMeshAgent pathFinder;
    // ���� ��� 

    // AI ������Ʈ ���� ����
    // pathFinder.isStopped = true/false;

    // AI ������ ���ϱ�
    //pathFinder.SetDestination(�������.transform.position);
    //pathFinder.SetDestination(targetEntity.transform.position);

    public GameObject hpBarPrefab;
    public GameObject damageTextPrefab;
    public Canvas canvasHpbar;
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0);
    public Slider enemyHpbar;
    private UIDamagePool damagePool;
    public TagUnitType player;

    /*
    public ParticleSystem hitEffect; //�ǰ� ����Ʈ
    public AudioClip deathSound;//��� ����
    public AudioClip hitSound; //�ǰ� ����
    */
    private Animator enemyAnimator;
    //private AudioSource enemyAudioPlayer; //����� �ҽ� ������Ʈ


    public float startingHealth = 20;
    public float health = 20;
    public float damage = 5f; //���ݷ�
    public float attackDelay = 1f; //���� ������
    private float lastAttackTime; //������ ���� ����
    private float dist; //���������� �Ÿ�

    private bool isDead = false; // �� ��� ����

    // �� ĳ������ ��Ʈ ó�� �� ������ �ؽ�Ʈ ǥ�� ����

    void Start()
    {
        SetHpBar();
        setRigidbodyState(true);
        setColliderState(false);
    }

    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        gameObject.GetComponent<Collider>().enabled = true;
    }
    public void Hit()
    {
        if (!isDead)
        {
            Debug.Log("����");
            health -= damage;
            CheckHp();
            if (damagePool != null)
            {
                GameObject damageUI = damagePool.GetObject();
                TextMeshProUGUI text = damageUI.GetComponent<TextMeshProUGUI>();
                text.text = damage.ToString();
                Debug.Log(text);
                Debug.Log("������ �α�");

                // UIDamageText ������Ʈ �ʱ�ȭ
                UIDamageText damageText = damageUI.GetComponent<UIDamageText>();
                damageText.Initialize(transform, Vector3.up * 2); // ���� ��ġ�� ������ ����

                StartCoroutine(ReturnDamageUIToPool(damageUI, 1f));
            }
            if (health <= 0)
            {
                isDead = true;
            }
        }
        if (isDead)
        {
            Debug.Log("����");
            // rbEnemybody.AddForce(new Vector3(0f, 1000f, 1000f));
            gameObject.GetComponent<Animator>().enabled = false;
            Destroy(gameObject, 5f);
            setRigidbodyState(false);
            setColliderState(true);
        }
    }
    private void CheckHp()
    {
        if (enemyHpbar != null)
        {
            enemyHpbar.value = health;
        }
    }
    private void SetHpBar()
    {
        canvasHpbar = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate(hpBarPrefab, canvasHpbar.transform);
        var hpBarScript = hpBar.GetComponent<UIEnemyHpBar>();
        hpBarScript.enemyTransform = transform;
        hpBarScript.offset = v3HpBar;

        enemyHpbar = hpBar.GetComponentInChildren<Slider>();
        enemyHpbar.maxValue = startingHealth;
        enemyHpbar.value = health;
    }
    private IEnumerator ReturnDamageUIToPool(GameObject damageUI, float delay)
    {
        yield return new WaitForSeconds(delay);
        damagePool.ReturnObject(damageUI);
    }
}