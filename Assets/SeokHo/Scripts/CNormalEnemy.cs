using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public enum State
{
    IDLE,   // ��� ����
    CHASE,  // ���� ����
    ATTACK, // ���� ����
    DIE, // ��� ����
}

public class CNormalEnemy : MonoBehaviour, IHittable
{
    #region ����
    public NavMeshAgent nmAgent; // ��� Ž���� ���� NavMeshAgent
    public GameObject hpBarPrefab; // HP �� ������
    public GameObject damageTextPrefab; // ������ �ؽ�Ʈ ������
    public GameObject bulletPrefab; // �߻�ü ������
    public Transform bulletSpawnPoint; // �߻�ü ���� ��ġ
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0); // HP �� ��ġ ������
    public Slider enemyHpbar; // ���� HP �� �����̴�
    private UIDamagePool damagePool; // ������ UI Ǯ
    private UIEnemyHpBar hpBarScript;
    private Animator animatorEnemy; // �ִϸ�����
    public TagUnitType player; // ���� ��� �±�
    public float damage; // ���ݷ�
    public float startingHealth; // ���� ü��
    private float health; // ���� ü��
    private float attackDelay = 3f; // ���� ����
    private float bulletSpeed = 10f; // �߻�ü �ӵ�
    public float attackRange; // ���� �Ÿ�
    public float chaseRange; // ���� �Ÿ�
    private float lastAttackTime; // ������ ���� ����
    private bool canMove;
    private bool canAttack;
    private bool isDead = false; // ��� ����
    private GameObject hpBarCanvas; // ���� HP �� ĵ����
    public State state; // ���� ����
    public Transform target; // ���� ���
    #endregion

    void Awake()
    {
        SetHpBar(); // HP �� ����
        setRigidbodyState(true); // Rigidbody ���� ����
        setColliderState(false); // Collider ���� ����

        damagePool = FindObjectOfType<UIDamagePool>(); // ������ Ǯ ã��
        nmAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ ��������
        animatorEnemy = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������

        state = State.IDLE; // �ʱ� ���¸� IDLE�� ����
        StartCoroutine(StateMachine()); // ���� �ӽ� ����
    }

    #region ���� �ӽ�
    // ���� �ӽ� �ڷ�ƾ
    public IEnumerator StateMachine()
    {
        while (health > 0)
        {
            switch (state)
            {
                case State.IDLE:
                    yield return StartCoroutine(IDLE());
                    break;
                case State.CHASE:
                    yield return StartCoroutine(CHASE());
                    break;
                case State.ATTACK:
                    yield return StartCoroutine(ATTACK());
                    break;
            }
        }
        switch (state)
        {
            case State.DIE:
                yield return StartCoroutine(DIE());
                break;
        }
    }

    // IDLE ���� �ڷ�ƾ
    private IEnumerator IDLE()
    {
        Debug.Log("Idle ����");
        animatorEnemy.Play("Idle");
        canMove = false;
        canAttack = false;
        while (state == State.IDLE)
        {
            yield return null;
        }
    }

    // CHASE ���� �ڷ�ƾ
    private IEnumerator CHASE()
    {
        Debug.Log("CHASE ����");
        canMove = true;
        canAttack = false;
        nmAgent.isStopped = false;
        nmAgent.SetDestination(target.position);

        while (state == State.CHASE)
        {
            if (target == null)
            {
                ChangeState(State.IDLE);
                yield break;
            }
            transform.LookAt(target);
            // �÷��̾ ��� ����
            nmAgent.SetDestination(target.position);

            // ���� �Ÿ��� ����Ͽ� ���� ���� ���� �ִ��� Ȯ��
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // ���� ���� ���� ������ ATTACK ���·� ��ȯ
            if (distanceToTarget <= attackRange)
            {
                nmAgent.isStopped = true;
                ChangeState(State.ATTACK);
            }
            // ���� ������ ����� IDLE ���·� ��ȯ
            else if (distanceToTarget > chaseRange)
            {
                target = null;
                nmAgent.SetDestination(transform.position);
                ChangeState(State.IDLE);
            }

            yield return null;
        }
    }

    // ATTACK ���� �ڷ�ƾ
    private IEnumerator ATTACK()
    {
        canAttack = true;
        if (target == null)
        {
            canAttack = false;
            ChangeState(State.IDLE);
            yield break;
        }

        // ���� �Ÿ��� ����Ͽ� ���� ���� ���� �ִ��� Ȯ��
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (!isDead && distanceToTarget <= attackRange)
        {
            canMove = false;

            if (lastAttackTime + attackDelay <= Time.time)
            {
                Debug.Log("ATTACK ����");
                transform.LookAt(target);


                lastAttackTime = Time.time;
                animatorEnemy.SetTrigger("IsShooting"); // �߻�ü �߻�
                yield return new WaitForSeconds(0.5f); // ���� �ִϸ��̼� �ð� ���
                ShootBullet(); // �߻�ü �߻�
            }
        }
        else
        {
            ChangeState(State.CHASE);
        }

        yield return null;
    }

    // DIE ���� �ڷ�ƾ
    private IEnumerator DIE()
    {
        Debug.Log("DIE ����");
        // �� ��� ���� ó��
        Destroy(hpBarCanvas, 1f);
        isDead = true;
        HandleDeath();
        yield return null;
    }

    // ���� ��ȯ �޼���
    public void ChangeState(State newState)
    {
        if (state == newState)
        {
            return;
        }
        state = newState;
    }
    #endregion

    void Update()
    {
        // ���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        animatorEnemy.SetBool("CanMove", canMove);
        // ���� �ִϸ��̼� ���
        animatorEnemy.SetBool("CanAttack", canAttack);
    }

    // �߻�ü �߻� �޼���
    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.velocity = (target.position - bulletSpawnPoint.position).normalized * bulletSpeed;

        //CEnemyBullet enemyBullet = bullet.GetComponent<CEnemyBullet>();
        //enemyBullet.damage = damage;
    }

    // �¾��� �� �޼���
    public void Hit(float damage)
    {
        ShowHpBar();
        // ���� �¾��� �� �������� ��
        if (state == State.IDLE)
        {
            ChangeState(State.CHASE);

        }

        GameObject damageUI = damagePool.GetObject();
        damage = Random.Range(5, 10);
        if (!isDead)
        {
            health -= damage;
            CheckHp();
            if (damagePool != null)
            {
                TextMeshProUGUI text = damageUI.GetComponent<TextMeshProUGUI>();
                text.text = (" - ") + damage.ToString();

                UIDamageText damageText = damageUI.GetComponent<UIDamageText>();
                damageText.Initialize(transform, Vector3.up * 2, damagePool);
            }
            if (health <= 0)
            {
                ChangeState(State.DIE);
            }
        }
    }

    #region Ragdoll, Collider ����
    // Rigidbody ���� ���� �޼���
    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
    }

    // Collider ���� ���� �޼���
    void setColliderState(bool state)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        gameObject.GetComponent<Collider>().enabled = true;
    }

    // �� ��� ó�� �޼���
    private void HandleDeath()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        Destroy(gameObject, 5f); // 5�� �Ŀ� ���� ������Ʈ �ı�
    }
    #endregion

    #region HPBar ����
    // HP Ȯ�� �� ������Ʈ �޼���
    private void CheckHp()
    {
        if (enemyHpbar != null)
        {
            enemyHpbar.value = health; // HP �� ������Ʈ
        }
    }

    // HP �� ���� �޼���
    private void SetHpBar()
    {
        health = startingHealth;
        // HP �ٰ� ���� �����̽� ĵ������ �����ǵ��� ����
        hpBarCanvas = new GameObject("EnemyHpBarCanvas");
        hpBarCanvas.transform.SetParent(transform);
        Canvas canvas = hpBarCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        CanvasScaler canvasScaler = hpBarCanvas.AddComponent<CanvasScaler>();
        canvasScaler.dynamicPixelsPerUnit = 10;

        GameObject hpBar = Instantiate(hpBarPrefab, canvas.transform);

        hpBarScript = hpBar.GetComponent<UIEnemyHpBar>();
        hpBarScript.trEnemy = transform;
        hpBarScript.v3offset = v3HpBar;
        enemyHpbar = hpBar.GetComponentInChildren<Slider>();
        enemyHpbar.maxValue = startingHealth;
        enemyHpbar.value = health;
        // hpBarScript = false;
    }
    private void ShowHpBar()
    {
        hpBarScript.enabled = true;
    }

    #endregion


}
