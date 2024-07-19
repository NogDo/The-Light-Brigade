using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerController : MonoBehaviour
{
    #region private 변수
    CPlayerStats playerStats;
    UIWeapon weaponUI;
    #endregion

    void Start()
    {
        playerStats = GetComponent<CPlayerStats>();
    }

    /// <summary>
    /// 플레이어 피격
    /// </summary>
    public void Hit(float damage)
    {
        playerStats.ChangeHP(playerStats.HP - (int)damage);
    }

    /// <summary>
    /// WeaponUI를 등록한다.
    /// </summary>
    public void SetWeaponUI(UIWeapon ui)
    {
        if (ui is null)
        {
            return;
        }

        weaponUI = ui;
        weaponUI.gameObject.SetActive(true);

        weaponUI.ChangeHPCount(playerStats.HP);
        weaponUI.ChangeHPUIColor((playerStats.HP >= 4) ? Color.white : Color.red);
    }
}
