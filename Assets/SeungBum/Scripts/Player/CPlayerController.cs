using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class CPlayerController : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    ActionBasedController leftController;
    [SerializeField]
    GameObject oTunnelingVignette;
    [SerializeField]
    AudioClip hitSound;

    CPlayerStats playerStats;
    UIWeapon weaponUI;
    UIPlayerStats playerStatsUI;

    bool isInitModel;
    #endregion

    public UIPlayerStats PlayerStatsUI
    {
        get
        {
            if (playerStatsUI is null)
            {
                return null;
            }

            else
            {
                return playerStatsUI;
            }
        }
    }

    void Start()
    {
        playerStats = GetComponent<CPlayerStats>();

        isInitModel = false;
        Debug.Log(playerStats.HP);
    }

    void LateUpdate()
    {
        InitModel();
    }

    /// <summary>
    /// Left Controller에 있는 Model의 UI를 할당받는다.
    /// </summary>
    void InitModel()
    {
        if (isInitModel)
        {
            return;
        }

        if (leftController.model != null)
        {
            playerStatsUI = leftController.model.GetComponentInChildren<UIPlayerStats>();
            playerStatsUI.gameObject.SetActive(false);
            playerStatsUI.ChangeLifeCount(playerStats.Life, playerStats.MaxLife);
            playerStatsUI.ChangeHPText(playerStats.HP, playerStats.MaxHP);
            playerStatsUI.ChangeSoulText(playerStats.Soul);
            playerStatsUI.ChangeAmmoText(playerStats.Ammo);

            GetComponent<CharacterController>().height = 1.5f;

            isInitModel = true;
        }
    }

    /// <summary>
    /// 플레이어 피격
    /// </summary>
    public void Hit(float damage)
    {
        CPlayerSoundManager.Instance.PlaySoundOneShot(hitSound);

        Debug.Log(playerStats.HP);
        playerStats.ChangeHP(playerStats.HP - (int)damage);
        playerStatsUI.ChangeHPText(playerStats.HP, playerStats.MaxHP);
        Debug.Log(damage);
        Debug.Log(playerStats.MaxHP);

        oTunnelingVignette.SetActive(true);
        if (playerStats.HP > 4)
        {
            Invoke("InActiveTunnelingVignette", 0.2f);
        }

        else
        {
            StopCoroutine("HealCooltime");
            StartCoroutine("HealCooltime");
        }


        if (playerStats.HP <= 0)
        {
            PlayerDie();
        }


        if (weaponUI is null)
        {
            return;
        }

        else
        {
            weaponUI.ChangeHPCount(playerStats.HP);
            weaponUI.ChangeHPUIColor((playerStats.HP >= 4) ? Color.white : Color.red);
        }
    }

    /// <summary>
    /// 플레이어 체력 회복
    /// </summary>
    /// <param name="healAmount">체력 회복량</param>
    public void Heal(int healAmount)
    {
        playerStats.ChangeHP(playerStats.HP + healAmount);
        playerStatsUI.ChangeHPText(playerStats.HP, playerStats.MaxHP);

        if (playerStats.HP > 4 && oTunnelingVignette.activeSelf)
        {
            Invoke("InActiveTunnelingVignette", 0.2f);
        }

        if (weaponUI is null)
        {
            return;
        }

        else
        {
            weaponUI.ChangeHPCount(playerStats.HP);
            weaponUI.ChangeHPUIColor((playerStats.HP >= 4) ? Color.white : Color.red);
        }
    }

    /// <summary>
    /// 플레이어 최대 체력을 증가시킨다.
    /// </summary>
    /// <param name="amount">증가시킬 체력량</param>
    public void IncreaseMaxHP(int amount)
    {
        playerStats.ChangeMaxHP(playerStats.MaxHP + amount);
        playerStats.ChangeHP(playerStats.HP + amount);
        PlayerStatsUI.ChangeHPText(playerStats.HP, playerStats.MaxHP);

        if (weaponUI is null)
        {
            return;
        }

        else
        {
            weaponUI.ChangeHPCount(playerStats.HP);
            weaponUI.ChangeHPUIColor((playerStats.HP >= 4) ? Color.white : Color.red);
        }
    }

    /// <summary>
    /// 플레이어 소울 소지량을 증가시키고, UI Text를 바꾸는 메서드
    /// </summary>
    /// <param name="soul">소울 획득량</param>
    public void AddSoul(int soul)
    {
        playerStats.ChangeSoul(playerStats.Soul + soul);
        playerStatsUI.ChangeSoulText(playerStats.Soul);
    }

    /// <summary>
    /// WeaponUI를 등록한다.
    /// </summary>
    public void SetWeaponUI(UIWeapon ui)
    {
        if (ui is null)
        {
            weaponUI = null;
            return;
        }

        weaponUI = ui;
        weaponUI.gameObject.SetActive(true);

        weaponUI.ChangeHPCount(playerStats.HP);
        weaponUI.ChangeHPUIColor((playerStats.HP >= 4) ? Color.white : Color.red);
    }

    /// <summary>
    /// 플레이어 피격시 화면이 빨개지는 효과를 비활성화 한다.
    /// </summary>
    void InActiveTunnelingVignette()
    {
        oTunnelingVignette.SetActive(false);
    }

    /// <summary>
    /// 플레이어 체력이 5미만 일 때, 일정 시간을 기다린 후 다시 체력을 5까지 회복하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator HealCooltime()
    {
        yield return new WaitForSeconds(10.0f);

        if (playerStats.HP < 5)
        {
            Heal(5 - playerStats.HP);
        }
    }

    /// <summary>
    /// 플레이어가 죽었을 때 실행되는 메서드
    /// </summary>
    void PlayerDie()
    {
        playerStats.DecreaseLife();
        playerStatsUI.ChangeLifeCount(playerStats.Life, playerStats.MaxLife);

        playerStats.ChangeHP(playerStats.MaxHP);
        Heal(playerStats.MaxHP);
        PlayerStatsUI.ChangeHPText(playerStats.HP, playerStats.MaxHP);

        if (playerStats.Life > 0)
        {
            SceneLoadManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        else
        {
            SceneLoadManager.Instance.LoadScene(0);
        }
    }
}
