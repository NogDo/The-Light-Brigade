using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CWolfEnemyAttack : MonoBehaviour
{
    private Collider AttackPoint;
    private CWolfEnemy wolfEnemy;

    private void Awake()
    {
        wolfEnemy = GetComponentInParent<CWolfEnemy>();
        AttackPoint = GetComponent<Collider>();

    }
    private void Start()
    {
        AttackPoint.enabled = true;
    }
    public void StartAttack()
    {
        AttackPoint.isTrigger = true;
    }

    public void EndAttack()
    {
        AttackPoint.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            // other.GetComponent<CPlayerController>();
            // 플레이어 컨트롤러 정보 가져오기
            if (other.TryGetComponent<CPlayerController>(out CPlayerController playerController))
            {
                wolfEnemy.damage = Random.Range(4.0f, 6.0f);
                float damage = wolfEnemy.damage;
                // 플레이어에 피해를 입힘
                playerController.Hit(damage);
            }
        }
    }
}
