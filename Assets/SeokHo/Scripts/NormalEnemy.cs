using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalEnemy : MonoBehaviour, IHittable
{
    //HpBarUI �߰��� ����
    public GameObject hpBarPrefab; //Instantiate �޼���� ������ �������� ���� ����
    public Canvas enemyHpBarCanvas;
    public Slider enemyHpBarSlider; //Slider�� �ʱ� ����, Hp ���ſ� ����� Slider�� ���� ����
    public Vector3 hpBarOffset = new Vector3(0, 2.4f, 0);
    private float startingHealth = 20;
    private float health = 20;
    private float damage = 5.0f;

    void Start()
    {
        SetHpBar();
    }

    public void CheckHp() // HP ����
    {
        if (enemyHpBarSlider != null)
            enemyHpBarSlider.value = health;
    }

    // �������� �Ծ��� �� ������ ó��
    public void Hit()
    {
        Debug.Log("����");
        // �ǰ� �ִϸ��̼� ���
        // enemyAnimator.SetTrigger("Hit");

        // �¾����Ƿ� ü�°���
        health -= damage;
        CheckHp();
        Debug.Log(enemyHpBarSlider.value);
    }


    //�� ��ġ + offset�� HpBarPrefab �����ϱ�
    void SetHpBar()
    {
        // Enemy HpBar Canvas��� ������Ʈ�� ���̾��Ű���� ã�Ƽ� Canvas ������Ʈ�� ������
        enemyHpBarCanvas = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();

        // hpBar �ν��Ͻ� ����
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, enemyHpBarCanvas.transform);

        var _hpbar = hpBar.GetComponent<EnemyHpBar>();
        _hpbar.enemyTr = gameObject.transform;
        _hpbar.offset = hpBarOffset;

        enemyHpBarSlider = hpBar.GetComponentInChildren<Slider>();

        // �����̴��� �ʱ� ���� ����
        enemyHpBarSlider.maxValue = startingHealth;
        enemyHpBarSlider.value = health;
    }
}

