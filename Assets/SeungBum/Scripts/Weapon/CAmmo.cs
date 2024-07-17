using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmo : MonoBehaviour
{
    #region protected 변수
    protected EWeapon equipWeaponType;
    protected int nBulletCount;
    #endregion

    /// <summary>
    /// 현재 Ammo를 착용할 수 있는 Weapon의 타입
    /// </summary>
    public EWeapon EquipWeaponType
    {
        get
        {
            return equipWeaponType;
        }
    }

    /// <summary>
    /// 탄창의 남은 총알 개수
    /// </summary>
    public int BulletCount
    {
        get
        {
            return nBulletCount;
        }
    }

    /// <summary>
    /// 현재 총알의 남은 개수를 줄인다.
    /// </summary>
    public void DecreaseBulltCount()
    {
        nBulletCount--;
    }
}
