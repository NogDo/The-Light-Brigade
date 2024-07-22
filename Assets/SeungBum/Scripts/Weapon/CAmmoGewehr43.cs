using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmoGewehr43 : CAmmo
{

    void OnEnable()
    {
        equipWeaponType = EWeapon.GEWEHR;
        nBulletMaxCount = 10;
        nBulletNowCount = 10;

        for (int i = 0; i < nBulletNowCount; i++)
        {
            oBullets[i].SetActive(true);
        }
    }
}