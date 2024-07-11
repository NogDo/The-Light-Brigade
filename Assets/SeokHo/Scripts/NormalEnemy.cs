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
    private float startingHealth = 22;
    private float health = 20;
    private float damage = 5;

    void Start()
    {
        SetHpBar();
    }


    // �������� �Ծ��� �� ������ ó��
    public void Hit()
    {
        Debug.Log("����");
        // �ǰ� �ִϸ��̼� ���
        // enemyAnimator.SetTrigger("Hit");

        // �¾����Ƿ� ü�°���
        health -= damage;
        // ü�� ����
        enemyHpBarSlider.value = health;
    }


    //�� ��ġ + offset�� HpBarPrefab �����ϱ�
    void SetHpBar()
    {
        enemyHpBarCanvas = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, enemyHpBarCanvas.transform);

        var _hpbar = hpBar.GetComponent<EnemyHpBar>();
        _hpbar.enemyTr = this.gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }


    //enemyHpBarSlider Ȱ��ȭ
    private void OnEnable()
    {
        //ü�� �����̴� Ȱ��ȭ
        enemyHpBarSlider.gameObject.SetActive(true);
        //ü�� �����̴��� �ִ��� �⺻ ü�°����� ����
        enemyHpBarSlider.maxValue = startingHealth;
    }
}
