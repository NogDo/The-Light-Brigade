using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossCircleIceShards : MonoBehaviour
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

        CBossTriggerRelay[] relays = GetComponentsInChildren<CBossTriggerRelay>();
        foreach (CBossTriggerRelay relay in relays)
        {
            relay.parentScript = this;
        }
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

        // 중간점을 계산
        Vector3 midPoint = (startPoint + targetPosition) / 2f;

        // Y축을 따라 반원형 경로를 생성하기 위한 오프셋 계산
        float heightOffset = Mathf.Sin(curveProgress * Mathf.PI) * curveRadius;
        Vector3 curvedPosition = Vector3.Lerp(startPoint, targetPosition, curveProgress);
        curvedPosition.y = startPoint.y + heightOffset;

        // 얼음 투사체를 새로운 곡선 위치로 이동
        transform.position = curvedPosition;
    }

    // 자식 객체의 트리거 이벤트를 처리하는 메서드
    public void OnChildTriggerEnter(Collider other)
    {
        // 플레이어와의 충돌 처리
        if (other.CompareTag("Player"))
        {
            CEnemySoundManager.Instance.PlayBossSound(2, transform.position);
            if (other.TryGetComponent<CPlayerController>(out CPlayerController playerController))
            {
                // 플레이어에게 데미지 적용
                playerController.Hit(damage);

                // 현재 위치에 파티클 이펙트 생성
                if (hitParticlePrefab != null)
                {
                    GameObject particleEffect = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
                    Destroy(particleEffect, 2f); // 지속 시간 조정
                }
            }
        }
        // 벽 또는 다른 객체와의 충돌 처리
        else if (other.CompareTag("Wall"))
        {
            // 파티클 이펙트 생성
            if (hitParticlePrefab != null)
            {
                GameObject particleEffect = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
                Destroy(particleEffect, 2f); // 지속 시간 조정
            }

        }
    }
}