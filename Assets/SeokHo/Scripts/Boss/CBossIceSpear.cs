using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossIceSpear : MonoBehaviour
{
    public float speed; // 창의 속도
    private Transform target;
    private float damage; // 데미지 변수 추가
    public GameObject hitParticlePrefab; // 충돌 시 재생될 파티클 시스템 프리팹

    private void Start()
    {
        // 3초 후에 객체를 파괴
        Destroy(gameObject, 3f);
    }

    // 타겟을 설정하는 메서드
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    // 데미지를 초기화하는 메서드
    public void Initialize(float damage)
    {
        this.damage = damage;
    }

    void Update()
    {
        // 타겟이 없으면 창을 파괴
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // 타겟을 향해 이동
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // 타겟 방향으로 회전
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌 처리
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<CPlayerController>(out CPlayerController playerController))
            {
                CEnemySoundManager.Instance.PlayBossSound(6, transform.position);
                CEnemySoundManager.Instance.PlayBossSound(7, transform.position);
                CEnemySoundManager.Instance.PlayBossSound(8, transform.position);
                // 플레이어에게 데미지 적용
                playerController.Hit(damage);

                // 파티클 이펙트를 현재 위치와 회전으로 인스턴스화
                if (hitParticlePrefab != null)
                {
                    Vector3 particlePosition = transform.position;
                    particlePosition.y -= 1; // y축 위치를 -1로 조정
                    GameObject particleEffect = Instantiate(hitParticlePrefab, particlePosition, Quaternion.identity);
                    Destroy(particleEffect, 2f); // 파티클 효과의 지속 시간 조정
                }

                // 창 객체를 일정 시간 후에 파괴
                Destroy(gameObject);
            }
        }
        // 벽 또는 다른 객체와의 충돌 처리
        else if (other.CompareTag("Wall"))
        {
            // 비 플레이어 객체와 충돌 시 파티클 이펙트 재생
            if (hitParticlePrefab != null)
            {
                CEnemySoundManager.Instance.PlayBossSound(7, transform.position);
                CEnemySoundManager.Instance.PlayBossSound(8, transform.position);
                Vector3 particlePosition = transform.position;
                particlePosition.y -= 1; // y축 위치를 -1로 조정
                GameObject particleEffect = Instantiate(hitParticlePrefab, particlePosition, Quaternion.identity);
                Destroy(particleEffect, 2f); // 파티클 효과의 지속 시간 조정
            }

            // 창 객체를 일정 시간 후에 파괴
            Destroy(gameObject);
        }
    }
}
