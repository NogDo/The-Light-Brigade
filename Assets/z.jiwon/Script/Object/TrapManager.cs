using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public GameObject openTrap;
    public GameObject closedTrap;

        
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("collider in");
            openTrap.SetActive(false);
            closedTrap.SetActive(true);

            if (other.transform.TryGetComponent<CPlayerController>(out CPlayerController playerController))
            {
                playerController.Hit(5);
            }
        }
    }
}
