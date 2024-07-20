using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CPlayerController : MonoBehaviour
{
    #region private ����
    [SerializeField]
    ActionBasedController leftController;

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
    }

    void LateUpdate()
    {
        InitModel();
    }

    /// <summary>
    /// Left Controller�� �ִ� Model�� UI�� �Ҵ�޴´�.
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
            playerStatsUI.ChangeMoneyCount(playerStats.Money);
            playerStatsUI.ChangePrayState(false);
            playerStatsUI.ChangeHPText(playerStats.HP, playerStats.MaxHP);
            playerStatsUI.ChangeSoulText(playerStats.Soul);
            playerStatsUI.ChangeAmmoText(playerStats.Ammo);

            isInitModel = true;
        }
    }

    /// <summary>
    /// �÷��̾� �ǰ�
    /// </summary>
    public void Hit(float damage)
    {
        playerStats.ChangeHP(playerStats.HP - (int)damage);

        if (weaponUI is null)
        {
            return;
        }

        else
        {
            weaponUI.ChangeHPCount(playerStats.HP);
            weaponUI.ChangeHPUIColor((playerStats.HP >= 4) ? Color.white : Color.red);
        }

        playerStatsUI.ChangeHPText(playerStats.HP, playerStats.MaxHP);
    }

    /// <summary>
    /// WeaponUI�� ����Ѵ�.
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
}
