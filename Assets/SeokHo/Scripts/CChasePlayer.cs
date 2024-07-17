using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChasePlayer : MonoBehaviour
{
    CNormalEnemy normalenemy;

    private void Start()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        normalenemy = transform.parent.GetComponent<CNormalEnemy>();
    }

    // 탐지 거리 10f
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        else if(other.CompareTag("Player"))
        {
            Debug.Log("닿았음");
            if(normalenemy == null)
            {
                return;
            }
            // 공격 상태로 전환
            normalenemy.ChangeState(State.CHASE);
            normalenemy.target = other.transform;
        }

    }
}
