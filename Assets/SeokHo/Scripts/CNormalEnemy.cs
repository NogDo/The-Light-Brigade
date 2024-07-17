using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    #region ����
    private NavMeshAgent pathFinder; // ��� Ž���� ���� NavMeshAgent
    public GameObject hpBarPrefab; // HP �� ������
    public GameObject damageTextPrefab; // ������ �ؽ�Ʈ ������
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0); // HP �� ��ġ ������
    public Slider enemyHpbar; // ���� HP �� �����̴�
    private UIDamagePool damagePool; // ������ UI Ǯ
    private Animator animatorNormalenemy; // �ִϸ�����

    public TagUnitType player; // ���� ��� �±�
    public float damage = 5f; // ���ݷ�
    public float health = 20; // ���� ü��
    public float startingHealth = 20; // ���� ü��

    public float attackDelay = 1f; // ���� ����
    private float lastAttackTime; // ������ ���� ����
    private float dist; // ���� ���� ������ �Ÿ�

    private bool isDead = false; // ��� ����
    private GameObject hpBarCanvas; // ���� HP �� ĵ����
    #endregion

    #region ���� ���
    private enum State
    {
        IDLE,   // ��� ����
        CHASE,  // ���� ����
        ATTACK, // ���� ����
        KILLED  // ��� ����
    }

    private State state; // ���� ����
    private Transform target; // ���� ���
    #endregion

    void Start()
    {
        SetHpBar(); // HP �� ����
        setRigidbodyState(true); // Rigidbody ���� ����
        setColliderState(false); // Collider ���� ����

        damagePool = FindObjectOfType<UIDamagePool>(); // ������ Ǯ ã��
        pathFinder = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ ��������
        animatorNormalenemy = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������

        state = State.IDLE; // �ʱ� ���¸� IDLE�� ����
        StartCoroutine(StateMachine()); // ���� ��� ����
    }

    private IEnumerator StateMachine()
    {
        while (health > 0) // ü���� 0 �̻��� �� ��� ����
        {
            yield return StartCoroutine(state.ToString()); // ���� ���� ����
        }
    }

    private IEnumerator IDLE()
    {
        animatorNormalenemy.Play("IdleRifle"); // Idle �ִϸ��̼� ���

        while (state == State.IDLE)
        {
            // IDLE ���¿��� �� �� (��: �ֺ��� �ѷ�����)
            yield return null;
        }
    }

    private IEnumerator CHASE()
    {
        animatorNormalenemy.Play("WalkFWD"); // �ȱ� �ִϸ��̼� ���

        while (state == State.CHASE)
        {
            if (target == null)
            {
                ChangeState(State.IDLE); // ����� ������ IDLE ���·� ��ȯ
                yield break;
            }

            pathFinder.SetDestination(target.position); // ��� ��ġ�� �̵�

            if (pathFinder.remainingDistance <= pathFinder.stoppingDistance)
            {
                ChangeState(State.ATTACK); // ��� �����ϸ� ATTACK ���·� ��ȯ
            }
            else if (Vector3.Distance(transform.position, target.position) > pathFinder.stoppingDistance * 2)
            {
                target = null;
                ChangeState(State.IDLE); // ����� �־����� IDLE ���·� ��ȯ
            }

            yield return null;
        }
    }

    private IEnumerator ATTACK()
    {
        animatorNormalenemy.Play("Attack01"); // ���� �ִϸ��̼� ���

        while (state == State.ATTACK)
        {
            if (target == null)
            {
                ChangeState(State.IDLE); // ����� ������ IDLE ���·� ��ȯ
                yield break;
            }

            if (pathFinder.remainingDistance > pathFinder.stoppingDistance)
            {
                ChangeState(State.CHASE); // ����� �־����� CHASE ���·� ��ȯ
                yield break;
            }

            if (Time.time >= lastAttackTime + attackDelay)
            {
                // ���� ����
                lastAttackTime = Time.time;
                // �÷��̾ ��󿡰� ������ ����
            }

            yield return null;
        }
    }

    private IEnumerator KILLED()
    {
        // �� ��� ���� ó��
       
        yield return null;
    }

    private void ChangeState(State newState)
    {
        state = newState; // ���ο� ���·� ��ȯ
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform; // ��� ����
            ChangeState(State.CHASE); // ���� ���·� ��ȯ
        }
    }

    void Update()
    {
        if (target != null)
        {
            pathFinder.SetDestination(target.position); // ��� ��ġ�� �̵�
        }
    }

    // ���� ������ �޾��� ��
    public void Hit(float damage)
    {
        damage = Random.Range(5, 10);
        if (!isDead)
        {
            health -= damage;
            CheckHp(); // HP üũ
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
                ChangeState(State.KILLED); // ��� ���·� ��ȯ
                Destroy(hpBarCanvas, 1f);
                isDead = true;
                HandleDeath(); // ��� ó��
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
        Destroy(gameObject, 5f); // 5�� �Ŀ� ���� ������Ʈ �ı�
    }

    #endregion

    #region HPBar ����
    private void CheckHp()
    {
        if (enemyHpbar != null)
        {
            enemyHpbar.value = health; // HP �� ������Ʈ
        }
    }

    private void SetHpBar()
    {
        // HP �ٰ� ���� �����̽� ĵ������ �����ǵ��� ����
        hpBarCanvas = new GameObject("EnemyHpBarCanvas");
        hpBarCanvas.transform.SetParent(transform);
        Canvas canvas = hpBarCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        CanvasScaler canvasScaler = hpBarCanvas.AddComponent<CanvasScaler>();
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
