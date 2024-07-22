using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public BulletPool bulletPool;

    // 보스 관련 변수
    public State state;  // 현재 상태
    public float startingHealth; // 시작 체력
    private float health; //  현재 체력
    public float damage; // 공격력
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
        // 이동할 목표 위치 설정
        if (!hasMovedToPosition)
        {
            moveToPosition = transform.position + new Vector3(0, 10f, 5f); // 예시 위치
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

        Destroy(gameObject, 5f); // 5초 후에 게임 오브젝트 파괴
        yield return null;
    }
    #endregion

    public void ShootBullet()
    {
        // 총알 발사 로직 구현
    }

    public void Hit(float damage)
    {
        ShowHpBar();

        // 피해 처리
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
                moveToPosition = transform.position + new Vector3(0, 3f, 5f); // 예시 위치
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

                StartCoroutine(TriggerAnimationsLeft());
            }
            else if (randomChance < 0.6f) // 추가 30% 확률로 오른쪽 대쉬
            {
                StartCoroutine(TriggerAnimationsRight());
            }
            // 40% 확률로 아무것도 하지 않음
            else
            {
                // 아무 행동도 하지 않음
            }
        }
    }
    IEnumerator TriggerAnimationsLeft()
    {
        // 첫 번째 트리거 설정
        AnimatorBoss.SetTrigger("QuickDash_Left");

        // 잠시 기다리기 (애니메이션 상태가 변경될 시간을 확보)
        yield return new WaitForSeconds(0.1f);

        // 두 번째 트리거 설정
        AnimatorBossWing.SetTrigger("QuickDash_Left");
    }
    IEnumerator TriggerAnimationsRight()
    {
        // 첫 번째 트리거 설정
        AnimatorBoss.SetTrigger("QuickDash_Right");

        // 잠시 기다리기 (애니메이션 상태가 변경될 시간을 확보)
        yield return new WaitForSeconds(0.1f);

        // 두 번째 트리거 설정
        AnimatorBossWing.SetTrigger("QuickDash_Right");
    }

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
