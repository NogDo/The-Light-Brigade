using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossIceShards : MonoBehaviour
{
    private Transform target;
    public GameObject hitParticlePrefab; // 충돌 시 재생될 파티클 시스템 프리

    private void Start()
    {
         // 5초 후에 객체를 파괴
         Destroy(gameObject, 3f);
    }

    // 타겟을 설정하는 메서드
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        // 타겟이 없으면 오브젝트를 파괴
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        // 타겟 방향으로 회전
        transform.LookAt(target);
    }

    //private void OnParticleTrigger()
    //{
    //    Debug.Log("닿음");
    //    // 플레이어와 충돌 처리
    //    if (CompareTag("Player"))
    //    {
    //        if (TryGetComponent<CPlayerController>(out CPlayerController playerController))
    //        {
    //            // 플레이어에게 데미지 적용
    //            playerController.Hit(damage);

    //            // 파티클 이펙트를 현재 위치와 회전으로 인스턴스화
    //            if (hitParticlePrefab != null)
    //            {
    //                GameObject particleEffect = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
    //                Destroy(particleEffect, 2f); // 파티클 효과의 지속 시간 조정
    //            }
    //        }
    //    }
    //    // 벽 또는 다른 객체와의 충돌 처리
    //    else if (CompareTag("Wall"))
    //    {
    //        // 비 플레이어 객체와 충돌 시 파티클 이펙트 재생
    //        if (hitParticlePrefab != null)
    //        {
    //            GameObject particleEffect = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
    //            Destroy(particleEffect, 2f); // 파티클 효과의 지속 시간 조정
    //        }
    //    }
    //}
}
