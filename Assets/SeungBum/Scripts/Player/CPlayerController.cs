using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerController : MonoBehaviour
{
    #region private ����
    CPlayerStats playerStats;
    UIWeapon weaponUI;
    #endregion

    void Start()
    {
        playerStats = GetComponent<CPlayerStats>();
    }

    /// <summary>
    /// �÷��̾� �ǰ�
    /// </summary>
    public void Hit(float damage)
    {
        playerStats.ChangeHP(playerStats.HP - (int)damage);
    }

    /// <summary>
    /// WeaponUI�� ����Ѵ�.
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
