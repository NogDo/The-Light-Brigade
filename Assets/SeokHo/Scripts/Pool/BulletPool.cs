using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Queue<GameObject> bulletQueue;

    void Awake()
    {
        bulletQueue = new Queue<GameObject>();
    }

    public GameObject GetBullet()
    {
        if (bulletQueue.Count > 0)
        {
            GameObject bullet = bulletQueue.Dequeue();
            bullet.SetActive(true);
            bullet.transform.SetParent(null); // BulletPool���� �и�
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(transform); // BulletPool ������ �̵�
        bulletQueue.Enqueue(bullet);
    }
}
