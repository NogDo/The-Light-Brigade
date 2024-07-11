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
    private float startingHealth = 20;
    private float health = 20;
    private float damage = 5.0f;

    void Start()
    {
        SetHpBar();
    }

    public void CheckHp() // HP 갱신
    {
        if (enemyHpBarSlider != null)
            enemyHpBarSlider.value = health;
    }

    // 데미지를 입었을 때 실행할 처리
    public void Hit()
    {
        Debug.Log("맞음");
        // 피격 애니메이션 재생
        // enemyAnimator.SetTrigger("Hit");

        // 맞았으므로 체력감소
        health -= damage;
        CheckHp();
        Debug.Log(enemyHpBarSlider.value);
    }


    //적 위치 + offset에 HpBarPrefab 생성하기
    void SetHpBar()
    {
        // Enemy HpBar Canvas라는 오브젝트를 하이어라키에서 찾아서 Canvas 컴포넌트를 가져옴
        enemyHpBarCanvas = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();

        // hpBar 인스턴스 생성
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, enemyHpBarCanvas.transform);

        var _hpbar = hpBar.GetComponent<EnemyHpBar>();
        _hpbar.enemyTr = gameObject.transform;
        _hpbar.offset = hpBarOffset;

        enemyHpBarSlider = hpBar.GetComponentInChildren<Slider>();

        // 슬라이더의 초기 값을 설정
        enemyHpBarSlider.maxValue = startingHealth;
        enemyHpBarSlider.value = health;
    }
}

