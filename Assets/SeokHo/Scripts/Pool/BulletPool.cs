using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize;
    private Queue<GameObject> bullets;

    void Awake()
    {
        bullets = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);

            bullet.transform.SetParent(transform);

            bullets.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            GameObject bullet = bullets.Dequeue();
            if (bullet != null)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.SetParent(transform);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        if (bullet != null)
        {
            bullet.SetActive(false);
            bullet.transform.SetParent(transform);
            bullets.Enqueue(bullet);
        }
    }
}
