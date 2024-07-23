using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmoSTG44 : CAmmo
{
    void OnEnable()
    {
        equipWeaponType = EWeapon.STG44;
        nBulletMaxCount = 25;
        nBulletNowCount = 25;

        for (int i = 0; i < nBulletNowCount; i++)
        {
            oBullets[i].SetActive(true);
        }
    }
}