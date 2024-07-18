using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyBullet : MonoBehaviour
{
    public float damage = 5f;
    public float lifeTime = 5f; // �߻�ü�� ����

    void Start()
    {
        Destroy(gameObject, lifeTime); // ���� �ð� �� �߻�ü �ı�
    }

    private void OnCollisionEnter(Collision collision)
    {
        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittable.Hit(damage); // Ÿ�� ��� ������ ����
        }
        Destroy(gameObject); // �浹 �� �߻�ü �ı�
    }
}
