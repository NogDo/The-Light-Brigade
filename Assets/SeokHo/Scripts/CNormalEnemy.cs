using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public enum State
{
    IDLE,   // 대기 상태
    CHASE,  // 추적 상태
    ATTACK, // 공격 상태
    KILLED  // 사망 상태
}

public class CNormalEnemy : MonoBehaviour, IHittable
{
    CChasePlayer chaseplayer;
    #region 변수
    public NavMeshAgent nmAgent; // 경로 탐색을 위한 NavMeshAgent
    public GameObject hpBarPrefab; // HP 바 프리팹
    public GameObject damageTextPrefab; // 데미지 텍스트 프리팹
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0); // HP 바 위치 오프셋
    public Slider enemyHpbar; // 적의 HP 바 슬라이더
    private UIDamagePool damagePool; // 데미지 UI 풀
    private Animator animatorEnemy; // 애니메이터

    public TagUnitType player; // 추적 대상 태그
    public float damage = 5f; // 공격력
    public float health = 20; // 현재 체력
    public float startingHealth = 20; // 시작 체력

    public float attackDelay = 3f; // 공격 간격
    private float lastAttackTime; // 마지막 공격 시점
    private float dist; // 적과 추적 대상과의 거리
    public float attackRange; // 공격 사거리

    private bool canMove;
    private bool canAttack;

    private bool isDead = false; // 사망 여부
    private GameObject hpBarCanvas; // 개별 HP 바 캔버스
    #endregion

    // 추적 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하면
            if (target != null)
            {
                return true;
            }

            return false;
        }
    }

    #region 상태 머신

    public State state; // 현재 상태
    public Transform target; // 추적 대상

    #endregion

    void Awake()
    {
        SetHpBar(); // HP 바 설정
        setRigidbodyState(true); // Rigidbody 상태 설정
        setColliderState(false); // Collider 상태 설정

        damagePool = FindObjectOfType<UIDamagePool>(); // 데미지 풀 찾기
        nmAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트 가져오기
        animatorEnemy = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기

        state = State.IDLE; // 초기 상태를 IDLE로 설정
        StartCoroutine(StateMachine()); // 상태 머신 시작
    }

    public IEnumerator StateMachine()
    {
        while (health > 0) // 체력이 0 이상일 때 계속 실행
        {
            yield return StartCoroutine(state.ToString()); // 현재 상태 실행
        }
    }

    private IEnumerator IDLE()
    {
        var curAnimStateInfo = animatorEnemy.GetCurrentAnimatorStateInfo(0);

        Debug.Log("Idle 상태");
        animatorEnemy.Play("Idle"); // Idle 애니메이션 재생

        int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
        // 회전 속도 설정
        float lookSpeed = Random.Range(5f, 10f);

        for (float i = 0; i < curAnimStateInfo.length; i += Time.deltaTime)
        {
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + (dir) * Time.deltaTime * lookSpeed, 0f);
            // IDLE 상태에서 할 일 (예: 주변을 둘러보기)
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
        // 목표까지의 남은 거리가 멈추는 지점(10f)보다 작거나 같으면
        if (nmAgent.remainingDistance <= nmAgent.stoppingDistance)
        {
            ChangeState(State.ATTACK); // 대상에 도달하면 ATTACK 상태로 전환
        }
        // 탐지거리 밖이면 다시 IDLE
        else if (nmAgent.remainingDistance > 15)
        {
            target = null;
            nmAgent.SetDestination(transform.position); // 대상 위치로 이동
            yield return null;
            ChangeState(State.IDLE); // 대상이 멀어지면 IDLE 상태로 전환
        }
        else
        {
            yield return new WaitForSeconds(curAnimStateInfo.length);
        }
    }

    private IEnumerator ATTACK()
    {
        Debug.Log("ATTACK 상태");
        if (target == null)
        {
            canAttack = false;
            ChangeState(State.IDLE); // 대상이 없으면 IDLE 상태로 전환
            yield break;
        }

        // 자신이 사망하지 않고, 최근 공격 시점에서 attackDelay 이상 시간이 지났고,
        // 플레이어와의 거리가 공격 사거리안에 있다면 공격 가능
        if (!isDead && nmAgent.remainingDistance <= attackRange)
        {
            // 공격 반경 안에 있으면 움직임을 멈춤.
            canMove = false;
            this.transform.LookAt(target.transform);

            // 공격 딜레이가 지났다면 공격 애니메이션 실행
            if (lastAttackTime + attackDelay <= Time.time)
            {
                canAttack = true;
                lastAttackTime = Time.time; // 최근 공격 시간 초기화
            }
            // 공격 반경 안에 있지만, 딜레이가 남아있을 경우
            else
            {
                //canAttack = false;
            }
        }
        else
        {
            ChangeState(State.CHASE); // 대상이 멀어지면 CHASE 상태로 전환
        }

        yield return null;
        
    }

    private IEnumerator KILLED()
    {
        Debug.Log("KILLED 상태");
        // 적 사망 로직 처리

        yield return null;
    }

    public void ChangeState(State newState)
    {
        state = newState; // 새로운 상태로 전환
    }
    
    void Update()
    {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션 재생
        //animatorEnemy.SetBool("CanMove", canMove);

        // 공격 애니메이션 재생
        //animatorEnemy.SetBool("CanAttack", canAttack); 
    }

    // 적이 공격을 받았을 때
    public void Hit(float damage)
    {
        damage = Random.Range(5, 10);
        if (!isDead)
        {
            health -= damage;
            CheckHp(); // HP 체크
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
                ChangeState(State.KILLED); // 사망 상태로 전환
                Destroy(hpBarCanvas, 1f);
                isDead = true;
                HandleDeath(); // 사망 처리
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
        Destroy(gameObject, 5f); // 5초 후에 게임 오브젝트 파괴
    }

    #endregion

    #region HPBar 관련
    private void CheckHp()
    {
        if (enemyHpbar != null)
        {
            enemyHpbar.value = health; // HP 바 업데이트
        }
    }

    private void SetHpBar()
    {
        // HP 바가 월드 스페이스 캔버스에 생성되도록 설정
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
