using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmoGewehr43 : CAmmo
{
    void Start()
    {
        equipWeaponType = EWeapon.GEWEHR;
        nBulletCount = 10;
    }
}