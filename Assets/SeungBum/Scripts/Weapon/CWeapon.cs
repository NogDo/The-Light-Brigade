using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeapon
{
    GEWEHR
}

[System.Serializable]
public class CWeapon : CInteractable
{
    #region public ����
    public string oWeaponName;
    public float fDamage;
    public int nBulletMaxCount;
    public int nBulletNowCount;
    public int nWeaponRank;
    #endregion

    #region protected ����
    EWeapon weaponType;
    #endregion

    /// <summary>
    /// Interaction�� ������ ��ü�� ������ �ִ� ���� ActionNumber�� �� (Hand Animation�� �����ϱ� ���Ѱ�)
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
    /// Weapon�� ����
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
}