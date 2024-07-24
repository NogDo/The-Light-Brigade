using System.Collections;
using UnityEngine;

public class CBossSnowBall : MonoBehaviour
{
    public float speed; // 스노우볼의 이동 속도
    private Transform target; // 목표 플레이어의 Transform
    private float damage; // 스노우볼의 데미지
    public GameObject hitParticlePrefab; // 충돌 시 재생될 파티클 시스템 프리팹
    public Material snowBallMaterial; // 스노우볼의 재질
    private Renderer snowBallRenderer; // 스노우볼의 Renderer 컴포넌트

    private bool isVisible = false; // 스노우볼이 보이는지 여부


    private void Start()
    {
        // 스노우볼의 Renderer 컴포넌트를 가져옴
        snowBallRenderer = GetComponent<Renderer>();

        if (snowBallRenderer == null)
        {
            Debug.LogWarning("스노우볼에 Renderer 컴포넌트가 없습니다.");
            return;
        }

        // 스노우볼을 1.4초 동안 투명도 0에서 1로 변경
        StartCoroutine(HandleVisibilityAndMovement());
        // 3초 후에 객체를 파괴
        Destroy(gameObject, 3f);
    }

    // 목표 설정 메서드
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    // 데미지 초기화 메서드
    public void Initialize(float damage)
    {
        this.damage = damage;
    }

    private void Update()
    {
        // 스노우볼이 보이지 않으면 업데이트를 하지 않음
        if (!isVisible) return;

        // 목표가 없으면 스노우볼 제거
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // 목표를 향해 이동
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // 목표를 향해 회전
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {

        // 플레이어와 충돌 처리
        if (other.CompareTag("Player"))
        {
            CEnemySoundManager.Instance.PlayBossSound(10, transform.position);
            if (other.TryGetComponent<CPlayerController>(out CPlayerController playerController))
            {
                // 플레이어에게 데미지 적용
                playerController.Hit(damage);

                // 충돌 시 파티클 효과 생성
                if (hitParticlePrefab != null)
                {
                    Vector3 particlePosition = transform.position;
                    particlePosition.y -= 1; // y축 위치를 -1로 조정
                    GameObject particleEffect = Instantiate(hitParticlePrefab, particlePosition, Quaternion.identity);
                    Destroy(particleEffect, 2f); // 파티클 효과의 지속 시간 조정
                }

                // 스노우볼 제거
                Destroy(gameObject);
            }
        }
        // 벽이나 다른 오브젝트와 충돌 처리
        else if (other.CompareTag("Wall"))
        {
            CEnemySoundManager.Instance.PlayBossSound(10, transform.position);
            // 충돌 시 파티클 효과 생성
            if (hitParticlePrefab != null)
            {
                Vector3 particlePosition = transform.position;
                particlePosition.y -= 1; // y축 위치를 -1로 조정
                GameObject particleEffect = Instantiate(hitParticlePrefab, particlePosition, Quaternion.identity);
                Destroy(particleEffect, 2f); // 파티클 효과의 지속 시간 조정
            }

            // 스노우볼 제거
            Destroy(gameObject);
        }
    }

    // 투명도 처리를 위한 코루틴
    private IEnumerator HandleVisibilityAndMovement()
    {
        // 스노우볼을 완전 투명 상태로 설정
        if (snowBallMaterial != null && snowBallRenderer != null)
        {
            snowBallRenderer.material = snowBallMaterial;
            Color color = snowBallRenderer.material.color;
            color.a = 0f; // 완전 투명
            snowBallRenderer.material.color = color;

            // 1.4초 동안 투명도 0에서 1로 증가
            float elapsedTime = 0f;
            while (elapsedTime < 1.4f)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / 1.4f);
                snowBallRenderer.material.color = color;
                yield return null;
            }

            // 투명도 1로 설정
            color.a = 1f;
            snowBallRenderer.material.color = color;
        }

        // 스노우볼이 보이게 설정
        isVisible = true;
    }
}
