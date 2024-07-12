using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    //HpBarUI �߰��� ����
    public GameObject oHpBarPrefab; //Instantiate �޼���� ������ �������� ���� ����
    public GameObject oDamageTextprefab; //Instantiate �޼���� ������ �������� ���� ����
    public Canvas canvasEnemyhpbar; 
    public Canvas canvasEnemydamage;
    public Slider enemyHpBar; //Slider�� �ʱ� ����, Hp ���ſ� ����� Slider�� ���� ����
    private Transform trNormalEnemy;
    private TextMeshPro textDamage;
    private UIDamagePool damagePool;
    public Vector3 v3HpBarOffset = new Vector3(0, 2.4f, 0);

    private float fStartinghealth = 20;
    private float fHealth = 20;
    private float fDamage = 5.0f;

    void Start()
    {
        SetHpBar();
        textDamage = damagePool.GetComponent<TextMeshPro>();
    }

    public void CheckHp() // HP ����
    {
        if (enemyHpBar != null)
            enemyHpBar.value = fHealth;
    }

    // �������� �Ծ��� �� ������ ó��
    public void Hit()
    {
        // �ǰ� �ִϸ��̼� ���
        // enemyAnimator.SetTrigger("Hit");

        // �¾����Ƿ� ü�°���
        fHealth -= fDamage;
        CheckHp();
        damagePool.OutDamageText();
        
    }


    //�� ��ġ + offset�� HpBarPrefab �����ϱ�
    void SetHpBar()
    {
        // Enemy HpBar Canvas��� ������Ʈ�� ���̾��Ű���� ã�Ƽ� Canvas ������Ʈ�� ������
        canvasEnemyhpbar = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();

        // hpBar �ν��Ͻ� ����
        GameObject hpBar = Instantiate<GameObject>(oHpBarPrefab, canvasEnemyhpbar.transform);

        var _hpbar = hpBar.GetComponent<UIEnemyHpBar>();
        _hpbar.trEnemy = gameObject.transform;
        _hpbar.v3Offset = v3HpBarOffset;

        enemyHpBar = hpBar.GetComponentInChildren<Slider>();

        // �����̴��� �ʱ� ���� ����
        enemyHpBar.maxValue = fStartinghealth;
        enemyHpBar.value = fHealth;
    }
   
}

