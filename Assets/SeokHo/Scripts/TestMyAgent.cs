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
        //네비변수 초기화
        nav = GetComponent<NavMeshAgent>();
        //nav.enabled = false; //네비끄기
    }

    void Start()
    {

        //nav.enabled = true; //네비켜기
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            nav.SetDestination(target.position);

        
    }
}
