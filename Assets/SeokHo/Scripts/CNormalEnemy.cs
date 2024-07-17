using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public enum State
{
    IDLE,   // ��� ����
    CHASE,  // ���� ����
    ATTACK, // ���� ����
    KILLED  // ��� ����
}

public class CNormalEnemy : MonoBehaviour, IHittable
{
    CChasePlayer chaseplayer;
    #region ����
    public NavMeshAgent nmAgent; // ��� Ž���� ���� NavMeshAgent
    public GameObject hpBarPrefab; // HP �� ������
    public GameObject damageTextPrefab; // ������ �ؽ�Ʈ ������
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0); // HP �� ��ġ ������
    public Slider enemyHpbar; // ���� HP �� �����̴�
    private UIDamagePool damagePool; // ������ UI Ǯ
    private Animator animatorEnemy; // �ִϸ�����

    public TagUnitType player; // ���� ��� �±�
    public float damage = 5f; // ���ݷ�
    public float health = 20; // ���� ü��
    public float startingHealth = 20; // ���� ü��

    public float attackDelay = 3f; // ���� ����
    private float lastAttackTime; // ������ ���� ����
    private float dist; // ���� ���� ������ �Ÿ�
    public float attackRange; // ���� ��Ÿ�

    private bool canMove;
    private bool canAttack;

    private bool isDead = false; // ��� ����
    private GameObject hpBarCanvas; // ���� HP �� ĵ����
    #endregion

    // ���� ����� �����ϴ��� �˷��ִ� ������Ƽ
    private bool hasTarget
    {
        get
        {
            // ������ ����� �����ϸ�
            if (target != null)
            {
                return true;
            }

            return false;
        }
    }

    #region ���� �ӽ�

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

    public IEnumerator StateMachine()
    {
        while (health > 0) // ü���� 0 �̻��� �� ��� ����
        {
            yield return StartCoroutine(state.ToString()); // ���� ���� ����
        }
    }

    private IEnumerator IDLE()
    {
        var curAnimStateInfo = animatorEnemy.GetCurrentAnimatorStateInfo(0);

        Debug.Log("Idle ����");
        animatorEnemy.Play("Idle"); // Idle �ִϸ��̼� ���

        int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
        // ȸ�� �ӵ� ����
        float lookSpeed = Random.Range(5f, 10f);

        for (float i = 0; i < curAnimStateInfo.length; i += Time.deltaTime)
        {
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + (dir) * Time.deltaTime * lookSpeed, 0f);
            // IDLE ���¿��� �� �� (��: �ֺ��� �ѷ�����)
            yield return null;
        }
        
    }

    private IEnumerator CHASE()
    {
        var curAnimStateInfo = animatorEnemy.GetCurrentAnimatorStateInfo(0);
        canMove = true;
        animatorEnemy.SetBool("CanMove", true);
        canAttack = false;
        Debug.Log("CHASE");
        if(curAnimStateInfo.IsName("Walk")== false)
        {
            animatorEnemy.Play("Walk", 0, 0);
            yield return null;
        }
        // ��ǥ������ ���� �Ÿ��� ���ߴ� ����(10f)���� �۰ų� ������
        if (nmAgent.remainingDistance <= nmAgent.stoppingDistance)
        {
            ChangeState(State.ATTACK); // ��� �����ϸ� ATTACK ���·� ��ȯ
        }
        // Ž���Ÿ� ���̸� �ٽ� IDLE
        else if (nmAgent.remainingDistance > 15)
        {
            target = null;
            nmAgent.SetDestination(transform.position); // ��� ��ġ�� �̵�
            yield return null;
            ChangeState(State.IDLE); // ����� �־����� IDLE ���·� ��ȯ
        }
        else
        {
            yield return new WaitForSeconds(curAnimStateInfo.length);
        }
    }

    private IEnumerator ATTACK()
    {
        Debug.Log("ATTACK ����");
        if (target == null)
        {
            canAttack = false;
            ChangeState(State.IDLE); // ����� ������ IDLE ���·� ��ȯ
            yield break;
        }

        // �ڽ��� ������� �ʰ�, �ֱ� ���� �������� attackDelay �̻� �ð��� ������,
        // �÷��̾���� �Ÿ��� ���� ��Ÿ��ȿ� �ִٸ� ���� ����
        if (!isDead && nmAgent.remainingDistance <= attackRange)
        {
            // ���� �ݰ� �ȿ� ������ �������� ����.
            canMove = false;
            this.transform.LookAt(target.transform);

            // ���� �����̰� �����ٸ� ���� �ִϸ��̼� ����
            if (lastAttackTime + attackDelay <= Time.time)
            {
                canAttack = true;
                lastAttackTime = Time.time; // �ֱ� ���� �ð� �ʱ�ȭ
            }
            // ���� �ݰ� �ȿ� ������, �����̰� �������� ���
            else
            {
                //canAttack = false;
            }
        }
        else
        {
            ChangeState(State.CHASE); // ����� �־����� CHASE ���·� ��ȯ
        }

        yield return null;
        
    }

    private IEnumerator KILLED()
    {
        Debug.Log("KILLED ����");
        // �� ��� ���� ó��

        yield return null;
    }

    public void ChangeState(State newState)
    {
        state = newState; // ���ο� ���·� ��ȯ
    }
    
    void Update()
    {
        // ���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        //animatorEnemy.SetBool("CanMove", canMove);

        // ���� �ִϸ��̼� ���
        //animatorEnemy.SetBool("CanAttack", canAttack); 
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
