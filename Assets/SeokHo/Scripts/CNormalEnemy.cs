using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    #region 변수
    private NavMeshAgent pathFinder; // 경로 탐색을 위한 NavMeshAgent
    public GameObject hpBarPrefab; // HP 바 프리팹
    public GameObject damageTextPrefab; // 데미지 텍스트 프리팹
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0); // HP 바 위치 오프셋
    public Slider enemyHpbar; // 적의 HP 바 슬라이더
    private UIDamagePool damagePool; // 데미지 UI 풀
    private Animator animatorNormalenemy; // 애니메이터

    public TagUnitType player; // 추적 대상 태그
    public float damage = 5f; // 공격력
    public float health = 20; // 현재 체력
    public float startingHealth = 20; // 시작 체력

    public float attackDelay = 1f; // 공격 간격
    private float lastAttackTime; // 마지막 공격 시점
    private float dist; // 적과 추적 대상과의 거리

    private bool isDead = false; // 사망 여부
    private GameObject hpBarCanvas; // 개별 HP 바 캔버스
    #endregion

    #region 상태 기계
    private enum State
    {
        IDLE,   // 대기 상태
        CHASE,  // 추적 상태
        ATTACK, // 공격 상태
        KILLED  // 사망 상태
    }

    private State state; // 현재 상태
    private Transform target; // 추적 대상
    #endregion

    void Start()
    {
        SetHpBar(); // HP 바 설정
        setRigidbodyState(true); // Rigidbody 상태 설정
        setColliderState(false); // Collider 상태 설정

        damagePool = FindObjectOfType<UIDamagePool>(); // 데미지 풀 찾기
        pathFinder = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트 가져오기
        animatorNormalenemy = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기

        state = State.IDLE; // 초기 상태를 IDLE로 설정
        StartCoroutine(StateMachine()); // 상태 기계 시작
    }

    private IEnumerator StateMachine()
    {
        while (health > 0) // 체력이 0 이상일 때 계속 실행
        {
            yield return StartCoroutine(state.ToString()); // 현재 상태 실행
        }
    }

    private IEnumerator IDLE()
    {
        animatorNormalenemy.Play("IdleRifle"); // Idle 애니메이션 재생

        while (state == State.IDLE)
        {
            // IDLE 상태에서 할 일 (예: 주변을 둘러보기)
            yield return null;
        }
    }

    private IEnumerator CHASE()
    {
        animatorNormalenemy.Play("WalkFWD"); // 걷기 애니메이션 재생

        while (state == State.CHASE)
        {
            if (target == null)
            {
                ChangeState(State.IDLE); // 대상이 없으면 IDLE 상태로 전환
                yield break;
            }

            pathFinder.SetDestination(target.position); // 대상 위치로 이동

            if (pathFinder.remainingDistance <= pathFinder.stoppingDistance)
            {
                ChangeState(State.ATTACK); // 대상에 도달하면 ATTACK 상태로 전환
            }
            else if (Vector3.Distance(transform.position, target.position) > pathFinder.stoppingDistance * 2)
            {
                target = null;
                ChangeState(State.IDLE); // 대상이 멀어지면 IDLE 상태로 전환
            }

            yield return null;
        }
    }

    private IEnumerator ATTACK()
    {
        animatorNormalenemy.Play("Attack01"); // 공격 애니메이션 재생

        while (state == State.ATTACK)
        {
            if (target == null)
            {
                ChangeState(State.IDLE); // 대상이 없으면 IDLE 상태로 전환
                yield break;
            }

            if (pathFinder.remainingDistance > pathFinder.stoppingDistance)
            {
                ChangeState(State.CHASE); // 대상이 멀어지면 CHASE 상태로 전환
                yield break;
            }

            if (Time.time >= lastAttackTime + attackDelay)
            {
                // 공격 수행
                lastAttackTime = Time.time;
                // 플레이어나 대상에게 데미지 적용
            }

            yield return null;
        }
    }

    private IEnumerator KILLED()
    {
        // 적 사망 로직 처리
       
        yield return null;
    }

    private void ChangeState(State newState)
    {
        state = newState; // 새로운 상태로 전환
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform; // 대상 설정
            ChangeState(State.CHASE); // 추적 상태로 전환
        }
    }

    void Update()
    {
        if (target != null)
        {
            pathFinder.SetDestination(target.position); // 대상 위치로 이동
        }
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
