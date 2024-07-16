using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.AI;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    #region 변수
    public GameObject hpBarPrefab;
    public GameObject damageTextPrefab;
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0);
    public Slider enemyHpbar;
    private UIDamagePool damagePool;
    private Animator animatorNormalenemy;

    private NavMeshAgent agent;
    public Transform player;
    public float attackRange = 5.0f;

    public float damage = 5f; // 공격력
    public float health = 20;
    public float startingHealth = 20;

    public float attackDelay = 1f; // 공격 간격
    private float lastAttackTime; // 마지막 공격 시점
    private float dist; // 적과 추적대상과의 거리

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
        // 추적 대상의 존재 여부에 따라 다른 애니메이션 재생
        animatorNormalenemy.SetBool("CanMove", canMove);
        animatorNormalenemy.SetBool("CanAttack", canAttack);

        if (distanceToPlayer < attackRange)
        {
            canMove = false;
            canAttack = true;
            Debug.Log("Attack the player!");
            // 여기에서는 일반적으로 공격 애니메이션 또는 효과를 트리거합니다.

        }
        else
        {

            agent.SetDestination(player.position);
            canMove = true;
            canAttack = false;
        }
    }

    // 핵심 맞았을 시
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

}