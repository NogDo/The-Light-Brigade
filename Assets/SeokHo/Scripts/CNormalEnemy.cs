using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    //HpBarUI 추가한 변수
    public GameObject oHpBarPrefab; //Instantiate 메서드로 복제할 프리펩을 담을 변수
    public GameObject oDamageTextprefab; //Instantiate 메서드로 복제할 프리펩을 담을 변수
    public Canvas canvasEnemyhpbar; 
    public Canvas canvasEnemydamage;
    public Slider enemyHpBar; //Slider의 초기 세팅, Hp 갱신에 사용할 Slider를 담을 변수
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

    public void CheckHp() // HP 갱신
    {
        if (enemyHpBar != null)
            enemyHpBar.value = fHealth;
    }

    // 데미지를 입었을 때 실행할 처리
    public void Hit()
    {
        // 피격 애니메이션 재생
        // enemyAnimator.SetTrigger("Hit");

        // 맞았으므로 체력감소
        fHealth -= fDamage;
        CheckHp();
        damagePool.OutDamageText();
        
    }


    //적 위치 + offset에 HpBarPrefab 생성하기
    void SetHpBar()
    {
        // Enemy HpBar Canvas라는 오브젝트를 하이어라키에서 찾아서 Canvas 컴포넌트를 가져옴
        canvasEnemyhpbar = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();

        // hpBar 인스턴스 생성
        GameObject hpBar = Instantiate<GameObject>(oHpBarPrefab, canvasEnemyhpbar.transform);

        var _hpbar = hpBar.GetComponent<UIEnemyHpBar>();
        _hpbar.trEnemy = gameObject.transform;
        _hpbar.v3Offset = v3HpBarOffset;

        enemyHpBar = hpBar.GetComponentInChildren<Slider>();

        // 슬라이더의 초기 값을 설정
        enemyHpBar.maxValue = fStartinghealth;
        enemyHpBar.value = fHealth;
    }
   
}

