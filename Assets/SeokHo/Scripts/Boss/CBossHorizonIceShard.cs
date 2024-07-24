using System.Collections;
using UnityEngine;

public class CBossHorizonIceShard : MonoBehaviour
{
    public float speed; // 얼음 투사체 속도
    private Vector3 targetPosition; // 타겟 위치
    private float damage; // 데미지 값
    public GameObject hitParticlePrefab; // 충돌 시 재생될 파티클 시스템 프리팹
    private Vector3 startPoint; // 투사체의 시작 위치
    private float curveProgress = 0f; // 곡선 진행도

    private void Start()
    {
        // 3초 후에 객체를 파괴
        Destroy(gameObject, 3f);
    }

    // 타겟 위치 설정 메서드
    public void SetTarget(Vector3 targetPos)
    {
        targetPosition = targetPos;
        startPoint = transform.position; // 타겟이 설정될 때 시작 위치 기록
    }

    // 데미지 초기화 메서드
    public void Initialize(float damage)
    {
        this.damage = damage;
    }

    void Update()
    {
        // 반원형 경로 계산
        curveProgress += speed * Time.deltaTime;
        float curveRadius = Vector3.Distance(startPoint, targetPosition) / 2f;

        // 시작 위치와 타겟 위치의 중간점 계산
        Vector3 midPoint = (startPoint + targetPosition) / 2f;
        midPoint.y = startPoint.y; // 중간점의 높이는 시작 위치와 동일하게 설정

        // 반원형 경로를 생성하기 위한 오프셋 계산
        Vector3 offset = Vector3.Cross(targetPosition - startPoint, Vector3.up).normalized * curveRadius;

        // 보간을 사용하여 곡선 상의 위치 계산
        Vector3 curvedPosition = Vector3.Lerp(Vector3.Lerp(startPoint, midPoint + offset, curveProgress), Vector3.Lerp(midPoint + offset, targetPosition, curveProgress), curveProgress);

        // 얼음 투사체를 새로운 곡선 위치로 이동
        transform.position = curvedPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와의 충돌 처리
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<CPlayerController>(out CPlayerController playerController))
            {
                CEnemySoundManager.Instance.PlayBossSound(2, transform.position);
                // 플레이어에게 데미지 적용
                playerController.Hit(damage);

                // 현재 위치에 파티클 이펙트 생성
                if (hitParticlePrefab != null)
                {
                    GameObject particleEffect = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
                    Destroy(particleEffect, 2f); // 지속 시간 조정
                }

                // 얼음 투사체 파괴
                Destroy(gameObject);
            }
        }
        // 벽 또는 다른 객체와의 충돌 처리
        else if (other.CompareTag("Wall"))
        {
            CEnemySoundManager.Instance.PlayBossSound(3, transform.position);
            // 파티클 이펙트 생성
            if (hitParticlePrefab != null)
            {
                GameObject particleEffect = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
                Destroy(particleEffect, 2f); // 지속 시간 조정
            }

            // 얼음 투사체 파괴
            Destroy(gameObject);
        }
    }
}