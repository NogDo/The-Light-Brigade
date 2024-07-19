using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 10;
    private Queue<GameObject> bulletQueue;

    void Awake()
    {
        bulletQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            bulletQueue.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletQueue.Count > 0)
        {
            GameObject bullet = bulletQueue.Dequeue();
            bullet.SetActive(true);
            bullet.transform.SetParent(null); // BulletPool에서 분리
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
        bullet.transform.SetParent(transform); // BulletPool 하위로 이동
        bulletQueue.Enqueue(bullet);
    }
}
