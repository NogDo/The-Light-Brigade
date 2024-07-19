using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class CWolfEnemy : MonoBehaviour, IHittable
{
    #region 변수
    // UI 관련 변수
    public GameObject hpBarPrefab; // HP 바 프리팹
    public GameObject damageTextPrefab; // 데미지 텍스트 프리팹 
    public Vector3 v3HpBar; // HP 바 위치 오프셋
    public Slider enemyHpbar; // 적의 HP 바 슬라이더
    private UIDamagePool damagePool; // 데미지 UI 풀
    private UIEnemyHpBar hpBarScript;
    private GameObject hpBarCanvas; // 개별 HP 바 캔버스
    private Coroutine hideHpBarCoroutine;


    // 적 오브젝트 관련 변수
    public State state; // 현재 상태
    public float startingHealth; // 시작 체력
    public float damage; // 공격력
    public float attackRange; // 공격 사거리
    public float chaseRange; // 추적 거리
    public Transform target; // 추적 대상
    private NavMeshAgent nmAgent; // 경로 탐색을 위한 NavMeshAgent
    private Animator animatorEnemy; // 애니메이터
    private float health; // 현재 체력
    private float attackDelay = 5f; // 공격 간격
    private float bulletSpeed = 10f; // 발사체 속도
    private float lastAttackTime; // 마지막 공격 시점
    private bool canMove; // 추적가능여부
    private bool canAttack; // 공격가능여부
    private bool canWalk; // 대기가능여부
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

    #region 상태 머신

    // 상태 머신 코루틴
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

    // 상태 전환 메서드
    public void ChangeState(State newState)
    {
        if (state == newState)
        {
            return;
        }

        // 현재 상태가 IDLE 상태일 때, IDLE 코루틴을 중지
        if (state == State.IDLE)
        {
            StopCoroutine(IDLE());
            nmAgent.isStopped = true; // 이동을 멈추게 함
            nmAgent.SetDestination(transform.position); // 현재 위치로 목표를 설정하여 멈추게 함
        }

        state = newState;

        // 상태 전환 시 애니메이터 상태를 업데이트
        if (newState == State.IDLE)
        {
            animatorEnemy.Play("Idle");
        }
    }

    // IDLE 상태 코루틴
    private IEnumerator IDLE()
    {
        Debug.Log("Idle 상태");
        canMove = false;
        canAttack = false;

        // 애니메이터 상태를 IDLE로 설정
        animatorEnemy.Play("Idle");

        while (state == State.IDLE)
        {
            if (!nmAgent.hasPath || nmAgent.remainingDistance < 0.5f)
            {

                canWalk = true;
                Vector3 randomDirection = Random.insideUnitSphere * 10f; // 랜덤한 방향
                randomDirection += transform.position; // 현재 위치를 기준으로 랜덤 방향 계산

                NavMeshHit navHit;
                NavMesh.SamplePosition(randomDirection, out navHit, 5f, NavMesh.AllAreas); // NavMesh 상에서 랜덤 위치 계산

                Vector3 finalPosition = navHit.position; // 최종 목표 위치

                nmAgent.SetDestination(finalPosition); // 목표 위치 설정  
                nmAgent.isStopped = false; // 이동 활성화
            }

            yield return new WaitForSeconds(2f); // 일정 시간 대기 (랜덤 이동을 위한 대기 시간)
        }

        // 상태 전환 시, NavMeshAgent 멈추기 및 목표 취소
        nmAgent.isStopped = true;
        nmAgent.SetDestination(transform.position); // 현재 위치로 이동 목표를 설정하여 멈추게 함

    }

    // CHASE 상태 코루틴
    private IEnumerator CHASE()
    {
        Debug.Log("CHASE 상태");
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
            // 플레이어를 계속 추적
            nmAgent.SetDestination(target.position);

            // 남은 거리를 계산하여 공격 범위 내에 있는지 확인
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // 공격 범위 내에 들어오면 ATTACK 상태로 전환
            if (distanceToTarget <= attackRange)
            {
                nmAgent.isStopped = true;
                ChangeState(State.ATTACK);
            }

            yield return null;
        }
    }

    // ATTACK 상태 코루틴
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

        // 남은 거리를 계산하여 공격 범위 내에 있는지 확인
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            if (lastAttackTime + attackDelay <= Time.time)
            {
                Debug.Log("ATTACK 상태");

                lastAttackTime = Time.time;
                canAttack = true;
                yield return new WaitForSeconds(0.5f); // 공격 애니메이션 시간 대기
            }
        }
        else
        {
            ChangeState(State.CHASE);
        }

        yield return null;
    }

    // DIE 상태 코루틴
    private IEnumerator DIE()
    {
        Debug.Log("DIE 상태");
        // 적 사망 로직 처리
        Destroy(hpBarCanvas, 1f);
        HandleDeath();
        yield return null;
    }
    #endregion



    void Update()
    {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션 재생
        animatorEnemy.SetBool("CanMove", canMove);
        // 공격 애니메이션 재생
        animatorEnemy.SetBool("CanAttack", canAttack);
    }


    public void Hit(float damage)
    {
        ShowHpBar();

        // 만약 맞았을 시 대기상태일 때
        if (state == State.IDLE)
        {
            // 플레이어를 찾기 위해 탐색
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.transform; // 플레이어의 적절한 트랜스폼 참조
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




    #region Ragdoll, Collider 관련
    // Rigidbody 상태 설정 메서드
    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
    }

    // Collider 상태 설정 메서드
    void setColliderState(bool state)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        gameObject.GetComponent<Collider>().enabled = true;
    }

    // 적 사망 처리 메서드
    private void HandleDeath()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        Destroy(gameObject, 5f); // 5초 후에 게임 오브젝트 파괴
    }
    #endregion

    #region HPBar 관련
    // HP 확인 및 업데이트 메서드
    private void CheckHp()
    {
        if (enemyHpbar != null)
        {
            enemyHpbar.value = health; // HP 바 업데이트
        }
    }

    // HP 바 설정 메서드
    private void SetHpBar()
    {
        health = startingHealth;
        // HP 바가 월드 스페이스 캔버스에 생성되도록 설정
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

    // HP 바 표시 및 숨기기 메서드
    private void ShowHpBar()
    {
        if (hideHpBarCoroutine != null)
        {
            StopCoroutine(hideHpBarCoroutine);
        }

        hpBarCanvas.SetActive(true);
        hideHpBarCoroutine = StartCoroutine(HideHpBarAfterDelay(3f));
    }

    // 일정 시간 후 HP 바 숨기기 코루틴
    private IEnumerator HideHpBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hpBarCanvas.SetActive(false);
    }
    #endregion
}
