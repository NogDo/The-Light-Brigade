using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CBossEnemy : MonoBehaviour, IHittable
{
    #region ����
    // UI ���� ����
    public GameObject hpBarPrefab;  // HP �� ������
    public GameObject damageTextPrefab; // ������ �ؽ�Ʈ ������ 
    public Vector3 v3HpBar;  // HP �� ��ġ ������
    public Slider enemyHpbar; // ���� HP �� �����̴�
    private UIDamagePool damagePool; // ������ UI Ǯ
    private UIEnemyHpBar hpBarScript;
    private GameObject hpBarCanvas; // ���� HP �� ĵ����
    private Coroutine hideHpBarCoroutine;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public BulletPool bulletPool;

    // ���� ���� ����
    public State state;  // ���� ����
    public float startingHealth; // ���� ü��
    private float health; //  ���� ü��
    public float damage; // ���ݷ�
    public float attackRange; // ���� ��Ÿ�
    public Transform target; // ���� ���
    private Animator AnimatorBoss;
    public Animator AnimatorBossWing;
    private float attackDelay = 5f; // ���� ����
    private float lastAttackTime; // ������ ���� ����
    private Vector3 moveToPosition = new Vector3(0, 0, 0); // ������ �̵��� ������ ��ġ
    private bool hasMovedToPosition = false; // ������ �̵� �Ϸ��ߴ��� üũ�ϴ� ����
    private float moveSpeed = 1.5f; // ���� �̵� �ӵ�
    private float rotationSpeed = 5f; // ���� ȸ�� �ӵ�

    private enum AttackType
    {
        Attack1,
        Attack2,
        Attack3,
        Attack4,
        Attack5
    }
    #endregion

    void Awake()
    {
        SetHpBar();

        damagePool = FindObjectOfType<UIDamagePool>();

        AnimatorBoss = GetComponent<Animator>();

        // Ÿ�� �ڵ� �Ҵ�
        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.transform.GetChild(0);
            }
        }

        state = State.IDLE;
        StartCoroutine(StateMachine());
    }


    void Update()
    {
        // �÷��̾ �Ĵٺ��� ȸ��
        if (state == State.ATTACK && target != null)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // �÷��̾���� �Ÿ� ����
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > attackRange)
            {
                Vector3 moveDirection = directionToTarget * moveSpeed * Time.deltaTime;
                transform.position += moveDirection;
            }
        }
    }

    #region ���� �ӽ�
    public IEnumerator StateMachine()
    {
        while (health > 0)
        {
            switch (state)
            {
                case State.IDLE:
                    yield return StartCoroutine(IDLE());
                    break;
                case State.ATTACK:
                    yield return StartCoroutine(ATTACK());
                    break;
            }
        }
        if (state == State.DIE)
        {
            yield return StartCoroutine(DIE());
        }
    }

    public void ChangeState(State newState)
    {
        state = newState;
    }

    private IEnumerator IDLE()
    {
        while (state == State.IDLE)
        {

            yield return null;
        }
    }

    private IEnumerator ATTACK()
    {
        // �̵��� ��ǥ ��ġ ����
        if (!hasMovedToPosition)
        {
            moveToPosition = transform.position + new Vector3(0, 10f, 5f); // ���� ��ġ
            StartCoroutine(MoveToPosition());
            yield return null; // �̵��� �Ϸ�� ������ ��ٸ�
        }

        while (state == State.ATTACK)
        {
            if (target != null)  // target�� null�� �ƴ��� Ȯ��
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget <= attackRange)
                {
                    if (lastAttackTime + attackDelay <= Time.time)
                    {
                        transform.LookAt(target.GetChild(0));
                        lastAttackTime = Time.time;

                        AttackType attackType = (AttackType)Random.Range(0, 5);
                        switch (attackType)
                        {
                            case AttackType.Attack1:
                                AnimatorBoss.SetTrigger("Attack1");
                                break;
                            case AttackType.Attack2:
                                AnimatorBoss.SetTrigger("Attack2");
                                break;
                            case AttackType.Attack3:
                                AnimatorBoss.SetTrigger("Attack3");
                                break;
                            case AttackType.Attack4:
                                AnimatorBoss.SetTrigger("Attack4");
                                break;
                            case AttackType.Attack5:
                                AnimatorBoss.SetTrigger("Attack5");
                                break;
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
            yield return null;
        }
    }


    private IEnumerator DIE()
    {
        AnimatorBoss.Play("Death");
        AnimatorBossWing.Play("Death");
        AnimatorBossWing.Play("StandingIdle");

        Destroy(hpBarCanvas, 1f);

        Destroy(gameObject, 5f); // 5�� �Ŀ� ���� ������Ʈ �ı�
        yield return null;
    }
    #endregion

    public void ShootBullet()
    {
        // �Ѿ� �߻� ���� ����
    }

    public void Hit(float damage)
    {
        ShowHpBar();

        // ���� ó��
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
            return; // ü���� 0 ������ ��� �� �̻��� ó���� �ߴ�
        }

        // ��� ������ �� Hit�� ���� ���·� ��ȯ
        if (state == State.IDLE)
        {
            AnimatorBoss.SetTrigger("StartAttack");
            AnimatorBossWing.SetTrigger("StartAttack");

            if (!hasMovedToPosition)
            {
                // �̵��� ��ǥ ��ġ ���� (��: ������ ���� ��ġ���� �ణ ������ ��ġ)
                moveToPosition = transform.position + new Vector3(0, 3f, 5f); // ���� ��ġ
                StartCoroutine(MoveToPosition());
            }
            else
            {
                // �̵��� �Ϸ�� �� ���� ���·� ��ȯ
                ChangeState(State.ATTACK);
            }
        }
        // ���� ������ �� Hit��
        else if (state == State.ATTACK)
        {
            float randomChance = Random.value;

            if (randomChance < 0.3f) // 30% Ȯ���� ���� �뽬
            {

                StartCoroutine(TriggerAnimationsLeft());
            }
            else if (randomChance < 0.6f) // �߰� 30% Ȯ���� ������ �뽬
            {
                StartCoroutine(TriggerAnimationsRight());
            }
            // 40% Ȯ���� �ƹ��͵� ���� ����
            else
            {
                // �ƹ� �ൿ�� ���� ����
            }
        }
    }
    IEnumerator TriggerAnimationsLeft()
    {
        // ù ��° Ʈ���� ����
        AnimatorBoss.SetTrigger("QuickDash_Left");

        // ��� ��ٸ��� (�ִϸ��̼� ���°� ����� �ð��� Ȯ��)
        yield return new WaitForSeconds(0.1f);

        // �� ��° Ʈ���� ����
        AnimatorBossWing.SetTrigger("QuickDash_Left");
    }
    IEnumerator TriggerAnimationsRight()
    {
        // ù ��° Ʈ���� ����
        AnimatorBoss.SetTrigger("QuickDash_Right");

        // ��� ��ٸ��� (�ִϸ��̼� ���°� ����� �ð��� Ȯ��)
        yield return new WaitForSeconds(0.1f);

        // �� ��° Ʈ���� ����
        AnimatorBossWing.SetTrigger("QuickDash_Right");
    }

    private IEnumerator MoveToPosition()
    {
        hasMovedToPosition = true;

        // �̵� ����
        while (Vector3.Distance(transform.position, moveToPosition) > 0.5f)
        {
            Vector3 moveDirection = (moveToPosition - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            yield return null;
        }

        // �̵� �Ϸ� �� ��ġ ����
        transform.position = moveToPosition;

        // �̵� �� Attack ���·� ��ȯ
        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.transform.GetChild(0);
            }
        }

        ChangeState(State.ATTACK); // �̵��� �Ϸ�� �� ���� ��ȯ
    }


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
        health = startingHealth;
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
