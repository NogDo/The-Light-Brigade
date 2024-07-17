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

    // Ž�� �Ÿ� 10f
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        else if(other.CompareTag("Player"))
        {
            Debug.Log("�����");
            if(normalenemy == null)
            {
                return;
            }
            // ���� ���·� ��ȯ
            normalenemy.ChangeState(State.CHASE);
            normalenemy.target = other.transform;
        }

    }
}
