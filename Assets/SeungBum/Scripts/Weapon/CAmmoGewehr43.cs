using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmoGewehr43 : CAmmo
{
    void Start()
    {
        equipWeaponType = EWeapon.GEWEHR;
        nBulletMaxCount = 10;
        nBulletNowCount = 10;
    }
}