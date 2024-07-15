using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.AI;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    // 경로 계산 AI 에이전트 변수 선언
    private NavMeshAgent pathFinder;
    // 추적 대상 

    // AI 에이전트 정지 제어
    // pathFinder.isStopped = true/false;

    // AI 목적지 정하기
    //pathFinder.SetDestination(추적대상.transform.position);
    //pathFinder.SetDestination(targetEntity.transform.position);

    public GameObject hpBarPrefab;
    public GameObject damageTextPrefab;
    public Canvas canvasHpbar;
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0);
    public Slider enemyHpbar;
    private UIDamagePool damagePool;
    public TagUnitType player;

    /*
    public ParticleSystem hitEffect; //피격 이펙트
    public AudioClip deathSound;//사망 사운드
    public AudioClip hitSound; //피격 사운드
    */
    private Animator enemyAnimator;
    //private AudioSource enemyAudioPlayer; //오디오 소스 컴포넌트


    public float startingHealth = 20;
    public float health = 20;
    public float damage = 5f; //공격력
    public float attackDelay = 1f; //공격 딜레이
    private float lastAttackTime; //마지막 공격 시점
    private float dist; //추적대상과의 거리

    private bool isDead = false; // 적 사망 여부

    // 적 캐릭터의 히트 처리 및 데미지 텍스트 표시 관리

    void Start()
    {
        SetHpBar();
        setRigidbodyState(true);
        setColliderState(false);
    }

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
    public void Hit()
    {
        if (!isDead)
        {
            Debug.Log("맞음");
            health -= damage;
            CheckHp();
            if (damagePool != null)
            {
                GameObject damageUI = damagePool.GetObject();
                TextMeshProUGUI text = damageUI.GetComponent<TextMeshProUGUI>();
                text.text = damage.ToString();
                Debug.Log(text);
                Debug.Log("데미지 로그");

                // UIDamageText 컴포넌트 초기화
                UIDamageText damageText = damageUI.GetComponent<UIDamageText>();
                damageText.Initialize(transform, Vector3.up * 2); // 적의 위치와 오프셋 설정

                StartCoroutine(ReturnDamageUIToPool(damageUI, 1f));
            }
            if (health <= 0)
            {
                isDead = true;
            }
        }
        if (isDead)
        {
            Debug.Log("죽음");
            // rbEnemybody.AddForce(new Vector3(0f, 1000f, 1000f));
            gameObject.GetComponent<Animator>().enabled = false;
            Destroy(gameObject, 5f);
            setRigidbodyState(false);
            setColliderState(true);
        }
    }
    private void CheckHp()
    {
        if (enemyHpbar != null)
        {
            enemyHpbar.value = health;
        }
    }
    private void SetHpBar()
    {
        canvasHpbar = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate(hpBarPrefab, canvasHpbar.transform);
        var hpBarScript = hpBar.GetComponent<UIEnemyHpBar>();
        hpBarScript.enemyTransform = transform;
        hpBarScript.offset = v3HpBar;

        enemyHpbar = hpBar.GetComponentInChildren<Slider>();
        enemyHpbar.maxValue = startingHealth;
        enemyHpbar.value = health;
    }
    private IEnumerator ReturnDamageUIToPool(GameObject damageUI, float delay)
    {
        yield return new WaitForSeconds(delay);
        damagePool.ReturnObject(damageUI);
    }
}