using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHitExplosionParticlePool : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    GameObject oHitExplosionParticle;

    List<GameObject> HitExplosionParticlePool;

    int nIndex;
    #endregion

    void Start()
    {
        HitExplosionParticlePool = new List<GameObject>();

        for (int i = 0; i < 25; i++)
        {
            GameObject particle = Instantiate(oHitExplosionParticle, Vector3.zero, Quaternion.identity);
            particle.transform.SetParent(transform);
            particle.SetActive(false);

            HitExplosionParticlePool.Add(particle);
        }

        nIndex = 0;
    }

    /// <summary>
    /// HitExplosionParticle을 활성화한다.
    /// </summary>
    public void ActiveParticle(Vector3 spawnPoint)
    {
        if (nIndex >= HitExplosionParticlePool.Count)
        {
            nIndex = 0;
            HitExplosionParticlePool[nIndex].SetActive(true);
            HitExplosionParticlePool[nIndex].transform.position = spawnPoint;
            nIndex++;
        }

        else
        {
            HitExplosionParticlePool[nIndex].SetActive(true);
            HitExplosionParticlePool[nIndex].transform.position = spawnPoint;
            nIndex++;
        }
    }
}
