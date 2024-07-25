using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    IDLE,   // 대기 상태
    CHASE,  // 추적 상태
    ATTACK, // 공격 상태
    DIE, // 사망 상태
}

public class CBossEnemy : MonoBehaviour, IHittable
{
    #region 변수
    // UI 관련 변수
    public GameObject hpBarPrefab;  // HP 바 프리팹
    public GameObject damageTextPrefab; // 데미지 텍스트 프리팹 
    public Vector3 v3HpBar;  // HP 바 위치 오프셋
    public Slider enemyHpbar; // 적의 HP 바 슬라이더
    private UIDamagePool damagePool; // 데미지 UI 풀
    private UIEnemyHpBar hpBarScript;
    private GameObject hpBarCanvas; // 개별 HP 바 캔버스
    private Coroutine hideHpBarCoroutine;

    // 보스 관련 변수
    public State state;  // 현재 상태
    public float startingHealth; // 시작 체력
    private float health; //  현재 체력
    private float spearDamage; // 아이스 창 패턴 공격력
    private float snowBallDamage; // 스노우 볼 패턴 공격력
    private float iceShardDamage; // 얼음 조각 패턴 공격력
    private float IceCircleShardsDamage; // 얼음 조각 패턴 공격력

    public float attackRange; // 공격 사거리
    public Transform target; // 추적 대상
    private Animator AnimatorBoss;
    public Animator AnimatorBossWing;
    private float attackDelay = 5f; // 공격 간격
    private float lastAttackTime; // 마지막 공격 시점
    private Vector3 moveToPosition = new Vector3(0, 0, 0); // 보스가 이동할 고정된 위치
    private bool hasMovedToPosition = false; // 보스가 이동 완료했는지 체크하는 변수
    private float moveSpeed = 1.5f; // 보스 이동 속도
    private float rotationSpeed = 5f; // 보스 회전 속도
    public GameObject iceSpearPrefab; // 아이스 창 프리팹
    public GameObject snowBallPrefab; // 스노우 볼 프리팹
    public GameObject iceShardPrefab; // 얼음 조각 프리팹
    public GameObject iceShardsPrefab; // 여러 얼음 조각 프리팹
    public GameObject IceCircleShardsPrefab; // 원형 얼음 조각 프리팹
    public Animation iceSpear;
    public GameObject onPlayDefeateParticle;
    public GameObject onPlayDeathParticle;
    public GameObject onPlayBloodParticle;
    public GameObject soulPrefab; // 영혼 프리팹


    private enum AttackType
    {
        SpearAttack,
        SnowBallAttack,
        CircleAttack,
        IceShardsAttack,
        HorizontalAttack
    }
    #endregion

    void Awake()
    {
        SetHpBar();

        damagePool = FindObjectOfType<UIDamagePool>();

        AnimatorBoss = GetComponent<Animator>();

        // 타겟 자동 할당
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
        // 플레이어를 쳐다보며 회전
        if (state == State.ATTACK && target != null)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // 플레이어와의 거리 유지
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > attackRange)
            {
                Vector3 moveDirection = directionToTarget * moveSpeed * Time.deltaTime;
                transform.position += moveDirection;
            }
        }
    }

    #region 상태 머신
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
        // 이동할 목표 위치 설정
        if (!hasMovedToPosition)
        {
            moveToPosition = transform.position + new Vector3(0, 15f, 10f); // 예시 위치
            StartCoroutine(MoveToPosition());
            yield return null; // 이동이 완료될 때까지 기다림
        }

        while (state == State.ATTACK)
        {
            if (target != null)  // target이 null이 아닌지 확인
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget <= attackRange)
                {
                    if (lastAttackTime + attackDelay <= Time.time)
                    {
                        transform.LookAt(target.GetChild(0));
                        lastAttackTime = Time.time;

                        // 공격 타입 결정 및 애니메이션 트리거
                        AttackType attackType = (AttackType)Random.Range(0, 4); // 0부터 4까지의 랜덤 값
                        switch (attackType)
                        {
                            case AttackType.SpearAttack:
                                // SpearAttack 애니메이션 작동시 CreateIceSpear 호출
                                AnimatorBoss.SetTrigger("SpearAttack");
                                break;
                            case AttackType.SnowBallAttack:
                                AnimatorBoss.SetTrigger("SnowBallAttack");
                                break;
                            case AttackType.CircleAttack:
                                AnimatorBoss.SetTrigger("CircleAttack");
                                break;
                            case AttackType.IceShardsAttack:
                                AnimatorBoss.SetTrigger("IceShardsAttack");
                                break;
                            case AttackType.HorizontalAttack:
                                AnimatorBoss.SetTrigger("HorizontalAttack");
                                break;
                        }

                        // 애니메이션 실행 시간 고려
                        yield return new WaitForSeconds(6f); // 6초 대기
                    }
                }
            }
            yield return null;
        }
    }

    #endregion



    public void Hit(float damage)
    {
        ShowHpBar();

        // 피해 처리
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
            AnimatorBoss.SetTrigger("Death");

            return; // 체력이 0 이하일 경우 더 이상의 처리를 중단
        }

        // 대기 상태일 때 Hit시 전투 상태로 전환
        if (state == State.IDLE)
        {
            AnimatorBoss.SetTrigger("StartAttack");
            AnimatorBossWing.SetTrigger("StartAttack");

            if (!hasMovedToPosition)
            {
                // 이동할 목표 위치 설정 (예: 보스의 현재 위치에서 약간 떨어진 위치)
                moveToPosition = transform.position + new Vector3(0, 3f, 7f); // 예시 위치
                StartCoroutine(MoveToPosition());
            }
            else
            {
                // 이동이 완료된 후 공격 상태로 전환
                ChangeState(State.ATTACK);
            }
        }
        // 전투 상태일 때 Hit시
        else if (state == State.ATTACK)
        {
            float randomChance = Random.value;

            if (randomChance < 0.3f) // 30% 확률로 왼쪽 대쉬
            {
                AnimatorBoss.SetTrigger("QuickDash_Left");
            }
            else if (randomChance < 0.6f) // 추가 30% 확률로 오른쪽 대쉬
            {
                AnimatorBoss.SetTrigger("QuickDash_Right");
            }
            // 40% 확률로 아무것도 하지 않음
            else
            {
                // 아무 행동도 하지 않음
            }
        }
    }

    #region Animation 관련

    private IEnumerator MoveToPosition()
    {
        hasMovedToPosition = true;

        // 이동 로직
        while (Vector3.Distance(transform.position, moveToPosition) > 0.5f)
        {
            Vector3 moveDirection = (moveToPosition - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            yield return null;
        }

        // 이동 완료 후 위치 조정
        transform.position = moveToPosition;

        // 이동 후 Attack 상태로 전환
        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.transform.GetChild(0);
            }
        }

        ChangeState(State.ATTACK); // 이동이 완료된 후 상태 전환
    }

    // 1.공격 패턴 IceSpear 생성
    public void CreateIceSpear()
    {
        // IceSpear 프리팹이 비어있는지 확인
        if (iceSpearPrefab == null)
        {
            Debug.LogWarning("IceSpearPrefab이 할당되지 않았습니다.");
            return;
        }

        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 2.0f;

        // IceSpear 프리팹을 현재 위치에서 생성
        GameObject iceSpear = Instantiate(iceSpearPrefab, spawnPosition, Quaternion.identity);

        // IceSpear 에 방향과 타겟 설정
        CBossIceSpear iceSpearScript = iceSpear.GetComponent<CBossIceSpear>();
        if (iceSpearScript != null)
        {
            spearDamage = Random.Range(7.0f, 9.0f);
            iceSpearScript.Initialize(spearDamage);
            iceSpearScript.SetTarget(target);
        }
    }
    public void SpearAttack()
    {
        iceSpear.Play("SpearAttack");
    }

    // 2.공격 패턴 SnowBall 생성
    public void CreateSnowBall()
    {
        // SnowBall 프리팹이 비어있는지 확인
        if (snowBallPrefab == null)
        {
            Debug.LogWarning("SnowBallPrefab이 할당되지 않았습니다.");
            return;
        }

        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 4.4f;

        // SnowBall 프리팹을 현재 위치에서 생성
        GameObject snowBall = Instantiate(snowBallPrefab, spawnPosition, Quaternion.identity);

        // SnowBall 에 방향과 타겟 설정
        CBossSnowBall snowBallScript = snowBall.GetComponent<CBossSnowBall>();
        if (snowBallScript != null)
        {
            snowBallDamage = Random.Range(7.0f, 9.0f);
            snowBallScript.Initialize(snowBallDamage);
            snowBallScript.SetTarget(target);
        }
    }

    // 3.공격 패턴 수평으로 날아가는 얼음 투사체를 생성
    public void CreateHorizoniceShard()
    {
        // iceShardPrefab이 할당되었는지 확인
        if (iceShardPrefab == null)
        {
            Debug.LogWarning("iceShardPrefab이 할당되지 않았습니다.");
            return;
        }

        // 스폰 위치 설정 (현재 위치에서 y축으로 2.0f 위)
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 2.0f;

        // 타겟 위치를 생성 시점에 캡처
        Vector3 targetPosition = target.position;

        // 얼음 투사체의 초기 회전값을 (90, 90, 90)으로 설정
        Quaternion initialRotation = Quaternion.Euler(90, 90, 90);

        // 얼음 투사체 프리팹을 현재 위치에 생성하고 초기 회전값 설정
        GameObject iceShard = Instantiate(iceShardPrefab, spawnPosition, initialRotation);

        // 얼음 투사체의 방향과 타겟 설정
        CBossHorizonIceShard iceShardScript = iceShard.GetComponent<CBossHorizonIceShard>();
        if (iceShardScript != null)
        {
            iceShardDamage = Random.Range(4.0f, 6.0f);
            iceShardScript.Initialize(iceShardDamage);
            iceShardScript.SetTarget(targetPosition);
        }
    }


    // 4.공격 패턴 여러 얼음 투사체를 생성
    public void CreateIceShards()
    {
        if (iceShardsPrefab == null)
        {
            Debug.LogWarning("iceShardsPrefab이 할당되지 않았습니다.");
            return;
        }

        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 5f;


        // IceShards 프리팹을 현재 위치에서 생성
        GameObject IceShards = Instantiate(iceShardsPrefab, transform.position, Quaternion.identity);

        // IceShards 에 방향과 타겟 설정
        CBossIceShards IceShardsScript = IceShards.GetComponent<CBossIceShards>();
        if (IceShardsScript != null)
        {
            IceShardsScript.SetTarget(target);
        }
    }

    // 5.공격 패턴 원 형태의 얼음 투사체 생성
    public void CreateIceCircleShards()
    {
        // iceShardPrefab이 할당되었는지 확인
        if (IceCircleShardsPrefab == null)
        {
            Debug.LogWarning("IceCircleShardsPrefab이 할당되지 않았습니다.");
            return;
        }

        // 타겟 위치를 생성 시점에 캡처
        Vector3 targetPosition = target.position;

        // 얼음 투사체의 초기 회전값을 (90, 90, 90)으로 설정
        Quaternion initialRotation = Quaternion.Euler(90, 90, 90);

        // 얼음 투사체 프리팹을 현재 위치에 생성하고 초기 회전값 설정
        GameObject iceCircleShardsPrefab = Instantiate(IceCircleShardsPrefab, transform.position, initialRotation);

        // 얼음 투사체의 방향과 타겟 설정
        CBossCircleIceShards iceShardScript = iceCircleShardsPrefab.GetComponent<CBossCircleIceShards>();
        if (iceShardScript != null)
        {
            IceCircleShardsDamage = Random.Range(4.0f, 6.0f);
            iceShardScript.Initialize(IceCircleShardsDamage);
            iceShardScript.SetTarget(targetPosition);
        }
    }

    // 죽었을 때 파티클 애니메이션 이벤트
    public void OnPlayDeathParticles()
    {
        Vector3 particlePosition = transform.position;
        particlePosition.y += 1f; // y축 위치를 조정

        GameObject OnPlayBloodParticle = Instantiate(onPlayBloodParticle, particlePosition, Quaternion.identity);
        Destroy(OnPlayBloodParticle, 5f); // 파티클 효과의 지속 시간 조정
        GameObject OnPlayDeathParticle = Instantiate(onPlayDeathParticle, particlePosition, Quaternion.identity);
        Destroy(OnPlayDeathParticle, 3.5f); // 파티클 효과의 지속 시간 조정
        GameObject OnPlayDefeateParticle = Instantiate(onPlayDefeateParticle, particlePosition, Quaternion.identity);
        Destroy(OnPlayDefeateParticle, 5f); // 파티클 효과의 지속 시간 조정
    }

    // 죽었을 때 애니메이션 이벤트
    public void DieAnimation()
    {

        Destroy(hpBarCanvas, 1f);
        StartCoroutine(DieTime());
    }

    // 오브젝트 비활성화까지 시간
    private IEnumerator DieTime()
    {
        // 1초 후에 영혼 생성 및 이동 시작
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 3; i++)
        {
            GameObject soul = Instantiate(soulPrefab, transform.position, Quaternion.identity);
            StartCoroutine(MoveSoulToTarget(soul, target));
        }
        // 3초 대기
        yield return new WaitForSeconds(3.5f);
        gameObject.SetActive(false);

        Destroy(gameObject, 2.0f); // 몇 초 후에 게임 오브젝트 파괴
    }

    #endregion

    #region 사운드 관련

    private void BossDodgeSound()
    {
        CEnemySoundManager.Instance.PlayBossSound(0, transform.position);
    }
    private void BossThrowShardSound()
    {
        CEnemySoundManager.Instance.PlayBossSound(1, transform.position);
    }
    private void BossSpearCreateSound()
    {
        CEnemySoundManager.Instance.PlayBossSound(4, transform.position);
    }
    private void BossSpearThrowSound()
    {
        CEnemySoundManager.Instance.PlayBossSound(5, transform.position);
    }
    private void BossSnowBallSound()
    {
        CEnemySoundManager.Instance.PlayBossSound(9, transform.position);
    }
    private void BossDeathSound()
    {
        CEnemySoundManager.Instance.PlayBossSound(11, transform.position);
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
}
