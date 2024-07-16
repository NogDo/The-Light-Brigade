using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IHittable
{
    public float startingHealth = 50f;
    public float health { get; protected set; }
   
    public bool isDead { get; protected set; }
    public event Action onDeath;

    // 생명체가 활성화될 때 상태를 리셋
    protected virtual void OnEnable()
    {
        
        // 사망하지 않은 상태로 시작
        isDead = false;
        // 체력을 시작 체력으로 초기화
        health = startingHealth;
    }

    // 데미지를 입는 기능
    public virtual void Hit(float damage)
    {
        // 데미지만큼 체력 감소
        health -= damage;
        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if(health <= 0 && !isDead)
        {
            Die();
        }
        
    }

    public virtual void Die()
    {
        if(onDeath != null)
        {
            onDeath();
        }
        isDead = true;
    }
}
