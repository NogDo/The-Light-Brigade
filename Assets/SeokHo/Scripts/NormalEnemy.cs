using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalEnemy : MonoBehaviour, IHittable
{
    //HpBarUI 추가한 변수
    public GameObject hpBarPrefab; //Instantiate 메서드로 복제할 프리펩을 담을 변수
    public Canvas enemyHpBarCanvas;
    public Slider enemyHpBarSlider; //Slider의 초기 세팅, Hp 갱신에 사용할 Slider를 담을 변수
    public Vector3 hpBarOffset = new Vector3(0, 2.4f, 0);
    private float startingHealth = 22;
    private float health = 20;
    private float damage = 5;

    void Start()
    {
        SetHpBar();
    }


    // 데미지를 입었을 때 실행할 처리
    public void Hit()
    {
        Debug.Log("맞음");
        // 피격 애니메이션 재생
        // enemyAnimator.SetTrigger("Hit");

        // 맞았으므로 체력감소
        health -= damage;
        // 체력 갱신
        enemyHpBarSlider.value = health;
    }


    //적 위치 + offset에 HpBarPrefab 생성하기
    void SetHpBar()
    {
        enemyHpBarCanvas = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, enemyHpBarCanvas.transform);

        var _hpbar = hpBar.GetComponent<EnemyHpBar>();
        _hpbar.enemyTr = this.gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }


    //enemyHpBarSlider 활성화
    private void OnEnable()
    {
        //체력 슬라이더 활성화
        enemyHpBarSlider.gameObject.SetActive(true);
        //체력 슬라이더의 최댓값을 기본 체력값으로 변경
        enemyHpBarSlider.maxValue = startingHealth;
    }
}
