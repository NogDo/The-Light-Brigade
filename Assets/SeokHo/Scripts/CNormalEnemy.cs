using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.AI;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    #region 변수
    private NavMeshAgent pathFinder;
    public GameObject hpBarPrefab;
    public GameObject damageTextPrefab;
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0);
    public Slider enemyHpbar;
    private UIDamagePool damagePool;
    public TagUnitType player;
    private Animator enemyAnimator;

    public float startingHealth = 20;
    public float health = 20;
    public float damage = 5f;
    public float attackDelay = 1f;
    private float lastAttackTime;
    private float dist;

    private bool isDead = false;
    #endregion

    void Start()
    {
        SetHpBar();
        setRigidbodyState(true);
        setColliderState(false);

        damagePool = FindObjectOfType<UIDamagePool>();
    }

    public void Hit()
    {
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
                damageText.Initialize(transform, Vector3.up * 2, damagePool); // Pass the pool reference

                StartCoroutine(ReturnDamageUIToPool(damageUI, 1f));
            }
            if (health <= 0)
            {
                isDead = true;
                HandleDeath();
            }
        }
    }

    #region Ragdoll, Collider 관련
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

    #region HPBar 관련
    private void CheckHp()
    {
        if (enemyHpbar != null)
        {
            enemyHpbar.value = health;
        }
    }

    private void SetHpBar()
    {
        // HP 바가 월드 스페이스 캔버스에 생성되도록 설정
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

    // Damage Pool
    private IEnumerator ReturnDamageUIToPool(GameObject damageUI, float delay)
    {
        yield return new WaitForSeconds(delay);
        damagePool.ReturnObject(damageUI);
    }
}