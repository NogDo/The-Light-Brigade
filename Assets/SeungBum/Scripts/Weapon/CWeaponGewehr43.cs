using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponGewehr43 : CWeapon
{
    private void Start()
    {
        ActionNumber = 1;
        weaponType = EWeapon.GEWEHR;
        fShootCoolTime = 0.5f;
    }
}