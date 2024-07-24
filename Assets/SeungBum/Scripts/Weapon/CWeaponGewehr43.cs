using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponGewehr43 : CWeapon
{
    void Start()
    {
        ActionNumber = 1;
        weaponType = EWeapon.GEWEHR;
        fShootCoolTime = 0.5f;
        fDamage = 7.5f;
        fRecoilTime = 0.05f;
        nBulletMaxCount = 10;
        nBulletNowCount = 10;
    }
}