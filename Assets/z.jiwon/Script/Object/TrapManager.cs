using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    Animator trapAnimator;

    private void Start()
    {
        trapAnimator = GetComponent<Animator>();
    }
        
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trapAnimator.SetTrigger("leftclose");
            trapAnimator.SetTrigger("rightclose");
        }
    }
}
