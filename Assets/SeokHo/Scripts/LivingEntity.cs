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

    // ����ü�� Ȱ��ȭ�� �� ���¸� ����
    protected virtual void OnEnable()
    {
        
        // ������� ���� ���·� ����
        isDead = false;
        // ü���� ���� ü������ �ʱ�ȭ
        health = startingHealth;
    }

    // �������� �Դ� ���
    public virtual void Hit(float damage)
    {
        // ��������ŭ ü�� ����
        health -= damage;
        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
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
