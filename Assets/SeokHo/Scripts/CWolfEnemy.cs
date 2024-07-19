using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class CWolfEnemy : MonoBehaviour, IHittable
{
    #region ����
    // UI ���� ����
    public GameObject hpBarPrefab; // HP �� ������
    public GameObject damageTextPrefab; // ������ �ؽ�Ʈ ������ 
    public Vector3 v3HpBar; // HP �� ��ġ ������
    public Slider enemyHpbar; // ���� HP �� �����̴�
    private UIDamagePool damagePool; // ������ UI Ǯ
    private UIEnemyHpBar hpBarScript;
    private GameObject hpBarCanvas; // ���� HP �� ĵ����
    private Coroutine hideHpBarCoroutine;


    // �� ������Ʈ ���� ����
    public State state; // ���� ����
    public float startingHealth; // ���� ü��
    public float damage; // ���ݷ�
    public float attackRange; // ���� ��Ÿ�
    public float chaseRange; // ���� �Ÿ�
    public Transform target; // ���� ���
    private NavMeshAgent nmAgent; // ��� Ž���� ���� NavMeshAgent
    private Animator animatorEnemy; // �ִϸ�����
    private float health; // ���� ü��
    private float attackDelay = 5f; // ���� ����
    private float bulletSpeed = 10f; // �߻�ü �ӵ�
    private float lastAttackTime; // ������ ���� ����
    private bool canMove; // �������ɿ���
    private bool canAttack; // ���ݰ��ɿ���
    private bool canWalk; // ��Ⱑ�ɿ���
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

    // ���� ��ȯ �޼���
    public void ChangeState(State newState)
    {
        if (state == newState)
        {
            return;
        }

        // ���� ���°� IDLE ������ ��, IDLE �ڷ�ƾ�� ����
        if (state == State.IDLE)
        {
            StopCoroutine(IDLE());
            nmAgent.isStopped = true; // �̵��� ���߰� ��
            nmAgent.SetDestination(transform.position); // ���� ��ġ�� ��ǥ�� �����Ͽ� ���߰� ��
        }

        state = newState;

        // ���� ��ȯ �� �ִϸ����� ���¸� ������Ʈ
        if (newState == State.IDLE)
        {
            animatorEnemy.Play("Idle");
        }
    }

    // IDLE ���� �ڷ�ƾ
    private IEnumerator IDLE()
    {
        Debug.Log("Idle ����");
        canMove = false;
        canAttack = false;

        // �ִϸ����� ���¸� IDLE�� ����
        animatorEnemy.Play("Idle");

        while (state == State.IDLE)
        {
            if (!nmAgent.hasPath || nmAgent.remainingDistance < 0.5f)
            {

                canWalk = true;
                Vector3 randomDirection = Random.insideUnitSphere * 10f; // ������ ����
                randomDirection += transform.position; // ���� ��ġ�� �������� ���� ���� ���

                NavMeshHit navHit;
                NavMesh.SamplePosition(randomDirection, out navHit, 5f, NavMesh.AllAreas); // NavMesh �󿡼� ���� ��ġ ���

                Vector3 finalPosition = navHit.position; // ���� ��ǥ ��ġ

                nmAgent.SetDestination(finalPosition); // ��ǥ ��ġ ����  
                nmAgent.isStopped = false; // �̵� Ȱ��ȭ
            }

            yield return new WaitForSeconds(2f); // ���� �ð� ��� (���� �̵��� ���� ��� �ð�)
        }

        // ���� ��ȯ ��, NavMeshAgent ���߱� �� ��ǥ ���
        nmAgent.isStopped = true;
        nmAgent.SetDestination(transform.position); // ���� ��ġ�� �̵� ��ǥ�� �����Ͽ� ���߰� ��

    }

    // CHASE ���� �ڷ�ƾ
    private IEnumerator CHASE()
    {
        Debug.Log("CHASE ����");
        canAttack = false;
        canWalk = false;
        nmAgent.isStopped = false;
        nmAgent.SetDestination(target.position);

        while (state == State.CHASE)
        {
            canMove = true;
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

            yield return null;
        }
    }

    // ATTACK ���� �ڷ�ƾ
    private IEnumerator ATTACK()
    {
        canMove = false;
        canWalk = false;
        if (target == null)
        {
            canAttack = false;
            ChangeState(State.IDLE);
            yield break;
        }

        // ���� �Ÿ��� ����Ͽ� ���� ���� ���� �ִ��� Ȯ��
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            if (lastAttackTime + attackDelay <= Time.time)
            {
                Debug.Log("ATTACK ����");

                lastAttackTime = Time.time;
                canAttack = true;
                yield return new WaitForSeconds(0.5f); // ���� �ִϸ��̼� �ð� ���
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
        HandleDeath();
        yield return null;
    }
    #endregion



    void Update()
    {
        // ���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        animatorEnemy.SetBool("CanMove", canMove);
        // ���� �ִϸ��̼� ���
        animatorEnemy.SetBool("CanAttack", canAttack);
    }


    public void Hit(float damage)
    {
        ShowHpBar();

        // ���� �¾��� �� �������� ��
        if (state == State.IDLE)
        {
            // �÷��̾ ã�� ���� Ž��
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.transform; // �÷��̾��� ������ Ʈ������ ����
                ChangeState(State.CHASE);
            }
        }
        else if (state == State.CHASE)
        {
            if (target != null)
            {
                target = GameObject.FindGameObjectWithTag("Player")?.transform.GetChild(0);
            }
        }

        GameObject damageUI = damagePool.GetObject();
        damage = Random.Range(5, 10);
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

        hpBarCanvas.SetActive(false);
    }

    // HP �� ǥ�� �� ����� �޼���
    private void ShowHpBar()
    {
        if (hideHpBarCoroutine != null)
        {
            StopCoroutine(hideHpBarCoroutine);
        }

        hpBarCanvas.SetActive(true);
        hideHpBarCoroutine = StartCoroutine(HideHpBarAfterDelay(3f));
    }

    // ���� �ð� �� HP �� ����� �ڷ�ƾ
    private IEnumerator HideHpBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hpBarCanvas.SetActive(false);
    }
    #endregion
}
