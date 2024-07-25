using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySoul : MonoBehaviour
{
    int soul;

    private void OnEnable()
    {
        soul = Random.Range(2, 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CEnemySoundManager.Instance.PlayEnemySound(7, transform.position);
            if (other.TryGetComponent<CPlayerController>(out CPlayerController playerController))
            {
                playerController.AddSoul(soul);
            }
        }
    }
}
