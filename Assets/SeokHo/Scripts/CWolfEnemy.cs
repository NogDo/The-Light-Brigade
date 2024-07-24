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
    private float health; // 현재 체력
    public float damage; // 공격력
    public float attackRange; // 공격 사거리
    public float chaseRange; // 추적 거리
    public Transform target; // 추적 대상
    private NavMeshAgent nmAgent; // 경로 탐색을 위한 NavMeshAgent
    private Animator animatorEnemy; // 애니메이터
    private float attackDelay = 3f; // 공격 간격
    private float lastAttackTime; // 마지막 공격 시점
    private bool canMove; // 추적가능여부
    private bool canWalk; // 걷기가능여부
    private bool SeePlayer = true; // 적이 플레이어를 봤을 때
    public CWolfEnemyAttack wolfEnemyAttack;
    public GameObject soulPrefab; // 영혼 프리팹

    #endregion

    void Awake()
    {
        SetHpBar(); // HP 바 설정
        setRigidbodyState(true); // Rigidbody 상태 설정
        setColliderState(false); // Collider 상태 설정

        damagePool = FindObjectOfType<UIDamagePool>(); // 데미지 풀 찾기
        nmAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트 가져오기
        animatorEnemy = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
        wolfEnemyAttack = GetComponentInChildren<CWolfEnemyAttack>();

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
    }

    private IEnumerator IDLE()
    {
        nmAgent.speed = 1;
        canMove = false;

        // 애니메이터 상태를 IDLE로 설정
        animatorEnemy.Play("Idle");

        while (state == State.IDLE)
        {
            // NavMeshAgent에 경로가 없거나 남은 거리가 작으면
            if (!nmAgent.hasPath || nmAgent.remainingDistance < 0.5f)
            {
                // 애니메이터 상태를 WALK로 설정
                canWalk = true;

                // 랜덤한 방향 선택
                Vector3 randomDirection = Random.insideUnitSphere * 10f;
                randomDirection += transform.position;

                NavMeshHit navHit;
                if (NavMesh.SamplePosition(randomDirection, out navHit, 3f, NavMesh.AllAreas))
                {
                    Vector3 finalPosition = navHit.position;

                    // 목표 위치 설정 및 이동 시작
                    nmAgent.SetDestination(finalPosition);
                    nmAgent.isStopped = false;

                    // 이동 방향으로 캐릭터 회전
                    while (nmAgent.pathPending || nmAgent.remainingDistance > 0.5f)
                    {
                        // 목표 방향 계산
                        Vector3 direction = (finalPosition - transform.position).normalized;
                        if (direction != Vector3.zero)
                        {
                            // 회전 보간
                            Quaternion targetRotation = Quaternion.LookRotation(direction);
                            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                        }

                        yield return null; // 에이전트가 도착할 때까지 대기
                    }

                    // 도착 후 이동 멈추기 및 IDLE 애니메이션 재생
                    nmAgent.isStopped = true;
                    nmAgent.SetDestination(transform.position); // 현재 위치로 목표 설정하여 정지

                    // 50% 확률로 애니메이션 상태를 결정
                    float randomChance = Random.value; // 0.0f ~ 1.0f 범위의 랜덤 값
                    if (randomChance < 0.5f)
                    {
                        canWalk = false;
                        // IDLE 상태에서 5초 동안 대기
                        animatorEnemy.Play("Eat");
                        yield return new WaitForSeconds(1f);
                    }
                    else
                    {
                        canWalk = false;
                        // IDLE 상태에서 5초 동안 대기
                        animatorEnemy.Play("Idle");
                        yield return new WaitForSeconds(1f);
                    }
                }
            }

            yield return null; // 매 프레임 상태를 확인
        }

        // IDLE 상태를 벗어날 때, 에이전트를 멈추고 목표를 초기화
        nmAgent.isStopped = true;
        nmAgent.SetDestination(transform.position);

        // 애니메이터 상태를 IDLE로 설정
        animatorEnemy.Play("Idle");
    }


    // CHASE 상태 코루틴
    private IEnumerator CHASE()
    {
        if (SeePlayer)
        {   
            // 적이 플레이어를 봤을 때 소리
            WolfEnemySeePlayerSound();
            SeePlayer = false;
        }

        canWalk = false;
        nmAgent.speed = 3;
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
                yield break; // ATTACK 상태로 전환 후 CHASE 코루틴 종료
            }

            yield return null;
        }
    }

    // ATTACK 상태 코루틴
    private IEnumerator ATTACK()
    {
        if (SeePlayer)
        {   
            // 적이 플레이어를 봤을 때 소리
            WolfEnemySeePlayerSound();
            SeePlayer = false;
        }

        canWalk = false;
        canMove = false;
        while (state == State.ATTACK)
        {
            if (target == null)
            {
                ChangeState(State.IDLE);
                yield break;
            }

            // 남은 거리를 계산하여 공격 범위 내에 있는지 확인
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                if (lastAttackTime + attackDelay <= Time.time)
                {
                    animatorEnemy.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                    yield return new WaitForSeconds(0.5f); // 공격 애니메이션 시간 대기
                }
            }
            else
            {
                ChangeState(State.CHASE);
                yield break; // CHASE 상태로 전환 후 ATTACK 코루틴 종료
            }

            yield return null;
        }
    }

    // DIE 상태 코루틴
    private IEnumerator DIE()
    {
        WolfEnemyDieSound();
        // 적 사망 로직 처리
        nmAgent.isStopped = true;
        Destroy(hpBarCanvas, 1f);
        HandleDeath();

        // 1초 후에 영혼 생성 및 이동 시작
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 3; i++)
        {
            GameObject soul = Instantiate(soulPrefab, transform.position, Quaternion.identity);
            StartCoroutine(MoveSoulToTarget(soul, target));
        }
        yield return null;
    }
    #endregion

    void Update()
    {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션 재생
        animatorEnemy.SetBool("CanMove", canMove);
        animatorEnemy.SetBool("CanWalk", canWalk);
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
                target = GameObject.FindGameObjectWithTag("Player")?.transform;
            }
        }

        GameObject damageUI = damagePool.GetObject();
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

    // 공격 애니메이션이 작동하면 호출
    public void startjumpAttack()
    {
        wolfEnemyAttack.StartAttack();
    }

    public void endJumpAttack()
    {
        wolfEnemyAttack.EndAttack();
    }

    // 영혼 이동 코루틴
    private IEnumerator MoveSoulToTarget(GameObject soul, Transform target)
    {
        float fRandX = Random.Range(-2.0f, 2.0f) + transform.position.x;
        float fRandY = Random.Range(1.5f, 3.0f);
        float fRandZ = Random.Range(-2.0f, 2.0f) + transform.position.z;

        Vector3 v3StartPosition = soul.transform.position;
        Vector3 v3MiddlePosition = new Vector3(fRandX, fRandY, fRandZ);

        float fTime = 0.0f;
        float fDuration = 1f;

        while (fTime <= fDuration)
        {
            Vector3 lerpPoint1 = Vector3.Lerp(v3StartPosition, v3MiddlePosition, fTime / fDuration);
            Vector3 lerpPoint2 = Vector3.Lerp(v3MiddlePosition, target.position, fTime / fDuration);
            soul.transform.position = Vector3.Lerp(lerpPoint1, lerpPoint2, fTime / fDuration);

            fTime += Time.deltaTime;
            yield return null;
        }

        soul.transform.position = target.position;
        Destroy(soul);
    }

    #region 사운드 관련

    private void WolfEnemySeePlayerSound()
    {
        CEnemySoundManager.Instance.PlayEnemySound(4, transform.position);
    }

    private void WolfEnemyJumpAttackSound()
    {
        CEnemySoundManager.Instance.PlayEnemySound(5, transform.position);
    }

    private void WolfEnemyDieSound()
    {
        CEnemySoundManager.Instance.PlayEnemySound(6, transform.position);
    }

    #endregion

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
            hideHpBarCoroutine = null; // 코루틴 참조를 초기화합니다.
        }

        // HP 바가 파괴되었는지 확인합니다.
        if (hpBarCanvas != null)
        {
            hpBarCanvas.SetActive(true);
            hideHpBarCoroutine = StartCoroutine(HideHpBarAfterDelay(3f));
        }
    }

    // 일정 시간 후 HP 바 숨기기 코루틴
    private IEnumerator HideHpBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // HP 바가 파괴되었는지 확인합니다.
        if (hpBarCanvas != null)
        {
            hpBarCanvas.SetActive(false);
        }
    }
    #endregion

   
}
