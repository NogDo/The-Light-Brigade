using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponGewehr43 : CWeapon
{
    private void Start()
    {
        ActionNumber = 1;
        weaponType = EWeapon.GEWEHR;
        nGrabCount = 0;
    }
}