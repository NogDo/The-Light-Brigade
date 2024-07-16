using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.AI;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    #region ����
    public GameObject hpBarPrefab;
    public GameObject damageTextPrefab;
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0);
    public Slider enemyHpbar;
    private UIDamagePool damagePool;
    private Animator animatorNormalenemy;

    private NavMeshAgent agent;
    public Transform player;
    public float attackRange = 5.0f;

    public float damage = 5f; // ���ݷ�
    public float health = 20;
    public float startingHealth = 20;

    public float attackDelay = 1f; // ���� ����
    private float lastAttackTime; // ������ ���� ����
    private float dist; // ���� ���������� �Ÿ�

    private bool isDead = false;
    private bool canMove;
    private bool canAttack;
    #endregion

    private void Awake()
    {
        animatorNormalenemy = GetComponent<Animator>();
    }

    void Start()
    {
        SetHpBar();
        setRigidbodyState(true);
        setColliderState(false);
        agent = GetComponent<NavMeshAgent>();

        damagePool = FindObjectOfType<UIDamagePool>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        // ���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        animatorNormalenemy.SetBool("CanMove", canMove);
        animatorNormalenemy.SetBool("CanAttack", canAttack);

        if (distanceToPlayer < attackRange)
        {
            canMove = false;
            canAttack = true;
            Debug.Log("Attack the player!");
            // ���⿡���� �Ϲ������� ���� �ִϸ��̼� �Ǵ� ȿ���� Ʈ�����մϴ�.

        }
        else
        {

            agent.SetDestination(player.position);
            canMove = true;
            canAttack = false;
        }
    }

    // �ٽ� �¾��� ��
    public void Hit(float damage)
    {
        damage = Random.Range(5, 10);
        if (!isDead)
        {
            health -= damage;
            CheckHp();
            if (damagePool != null)
            {
                GameObject damageUI = damagePool.GetObject();
                TextMeshProUGUI text = damageUI.GetComponent<TextMeshProUGUI>();
                text.text = damage.ToString();

                UIDamageText damageText = damageUI.GetComponent<UIDamageText>();
                damageText.Initialize(transform, Vector3.up * 2, damagePool);
            }
            if (health <= 0)
            {
                GameObject trhp = GameObject.Find("EnemyHpBarCanvas");
                Destroy(trhp, 1f);
                isDead = true;
                HandleDeath();
            }
        }
    }

    #region Ragdoll, Collider ����
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

    private void HandleDeath()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        Destroy(gameObject, 5f);
    }

    #endregion

    #region HPBar ����
    private void CheckHp()
    {
        if (enemyHpbar != null)
        {
            enemyHpbar.value = health;
        }

    }

    private void SetHpBar()
    {
        // HP �ٰ� ���� �����̽� ĵ������ �����ǵ��� ����
        GameObject canvasObject = new GameObject("EnemyHpBarCanvas");
        canvasObject.transform.SetParent(transform);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.dynamicPixelsPerUnit = 10;

        GameObject hpBar = Instantiate(hpBarPrefab, canvas.transform);
        var hpBarScript = hpBar.GetComponent<UIEnemyHpBar>();
        hpBarScript.trEnemy = transform;
        hpBarScript.v3offset = v3HpBar;

        enemyHpbar = hpBar.GetComponentInChildren<Slider>();
        enemyHpbar.maxValue = startingHealth;
        enemyHpbar.value = health;
    }
    #endregion

}