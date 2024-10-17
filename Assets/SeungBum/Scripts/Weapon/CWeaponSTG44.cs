using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponSTG44 : CWeapon
{
    void Start()
    {
        weaponType = EWeapon.STG44;
        fShootCoolTime = 0.1f;
        fDamage = 2.0f;
        fRecoilTime = 0.03f;
    }
}
