using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemSpawn : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public Transform spawnPoint;

    public void SpawnItem(int index)
    {
        if (index >= 0 && index < prefabsToSpawn.Length)
        {
            Instantiate(prefabsToSpawn[index], spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("ÀÎµ¦½º°¡ Prefab ¹è¿­ÀÇ ¹üÀ§¸¦ ¹ş¾î³µ½À´Ï´Ù.");
        }
    }
}
