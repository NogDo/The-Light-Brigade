using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeapon
{
    GEWEHR = 1
}

[System.Serializable]
public class CWeapon : CInteractable
{
    #region public 변수
    public string oWeaponName;
    public float fDamage;
    public int nBulletMaxCount;
    public int nBulletNowCount;
    public int nWeaponRank;
    #endregion

    #region protected 변수
    protected EWeapon weaponType;
    protected int nGrabCount;
    #endregion

    /// <summary>
    /// Interaction이 가능한 물체가 가지고 있는 고유 ActionNumber의 값 (Hand Animation을 정의하기 위한것)
    /// </summary>
    public override int ActionNumber
    {
        get
        {
            return nActionNumber;
        }

        set
        {
            nActionNumber = value;
        }
    }

    /// <summary>
    /// Weapon의 종류
    /// </summary>
    public EWeapon WeaponType
    {
        get
        {
            return weaponType;
        }

        protected set
        {
            weaponType = value;
        }
    }

    /// <summary>
    /// 현재 Grab된 개수
    /// </summary>
    public int GrabCount
    {
        get
        {
            return nGrabCount;
        }

        set
        {
            nGrabCount = value;
        }
    }
}