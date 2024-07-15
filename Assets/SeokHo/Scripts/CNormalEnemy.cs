using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CNormalEnemy : MonoBehaviour, IHittable
{
    public GameObject hpBarPrefab;
    public GameObject damageTextPrefab;
    public Canvas canvasHpbar;
    public Canvas canvasDamage;
    public Vector3 v3HpBar = new Vector3(0, 2.4f, 0);
    public Slider enemyHpbar;
    private UIDamagePool damageUIPool;

    private float startingHealth = 20;
    private float health = 20;
    private float damage = 5;

    // 적 캐릭터의 히트 처리 및 데미지 텍스트 표시 관리

    void Start()
    {
        SetHpBar();

        if (canvasDamage != null)
        {
            damageUIPool = canvasDamage.GetComponent<UIDamagePool>();
            if (damageUIPool == null)
            {
                Debug.LogError("UIDamagePool component not found on damageCanvas");
            }
        }
        else
        {
            Debug.LogError("Damage Canvas is not assigned");
        }
    }

    public void Hit()
    {
        health -= damage;
        CheckHp();

        if (damageUIPool != null)
        {
            GameObject damageUI = damageUIPool.GetObject();
            TextMeshProUGUI text = damageUI.GetComponent<TextMeshProUGUI>();
            text.text = damage.ToString();

            // UIDamageText 컴포넌트 초기화
            UIDamageText damageText = damageUI.GetComponent<UIDamageText>();
            damageText.Initialize(transform, Vector3.up * 2); // 적의 위치와 오프셋 설정

            StartCoroutine(ReturnDamageUIToPool(damageUI, 1f));
        }
    }

    private void CheckHp()
    {
        if (enemyHpbar != null)
        {
            enemyHpbar.value = health;
        }
    }

    private void SetHpBar()
    {
        canvasHpbar = GameObject.Find("Enemy HpBar Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate(hpBarPrefab, canvasHpbar.transform);
        var hpBarScript = hpBar.GetComponent<UIEnemyHpBar>();
        hpBarScript.enemyTransform = transform;
        hpBarScript.offset = v3HpBar;

        enemyHpbar = hpBar.GetComponentInChildren<Slider>();
        enemyHpbar.maxValue = startingHealth;
        enemyHpbar.value = health;
    }

    private IEnumerator ReturnDamageUIToPool(GameObject damageUI, float delay)
    {
        yield return new WaitForSeconds(delay);
        damageUIPool.ReturnObject(damageUI);
    }
}