using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class TestMyAgent : MonoBehaviour
{
    NavMeshAgent nav;
    [SerializeField] Transform target;

    private void Awake()
    {
        //�׺񺯼� �ʱ�ȭ
        nav = GetComponent<NavMeshAgent>();
        //nav.enabled = false; //�׺����
    }

    void Start()
    {

        //nav.enabled = true; //�׺��ѱ�
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            nav.SetDestination(target.position);

        
    }
}
