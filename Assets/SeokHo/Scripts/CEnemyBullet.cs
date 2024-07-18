using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyBullet : MonoBehaviour
{
    public float damage = 5f;
    public float lifeTime = 5f; // 발사체의 수명

    void Start()
    {
        Destroy(gameObject, lifeTime); // 수명 시간 후 발사체 파괴
    }

    private void OnCollisionEnter(Collision collision)
    {
        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittable.Hit(damage); // 타격 대상에 데미지 적용
        }
        Destroy(gameObject); // 충돌 후 발사체 파괴
    }
}
