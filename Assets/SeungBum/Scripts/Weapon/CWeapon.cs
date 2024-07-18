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
    #region public ����
    public string oWeaponName;
    public float fDamage;
    public int nBulletMaxCount;
    public int nBulletNowCount;
    public int nWeaponRank;
    #endregion

    #region protected ����
    protected EWeapon weaponType;

    protected float fShootCoolTime;
    #endregion

    /// <summary>
    /// Interaction�� ������ ��ü�� ������ �ִ� ���� ActionNumber�� �� (Hand Animation�� �����ϱ� ���Ѱ�), ���� ������ ����
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
    /// ���� ���� �Ѿ��� ����
    /// </summary>
    public int BulletCount
    {
        get
        {
            return nBulletNowCount;
        }
    }

    /// <summary>
    /// ���� ������
    /// </summary>
    public float Damage
    {
        get
        {
            return fDamage;
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

    /// <summary>
    /// �� �߻� ��Ÿ��
    /// </summary>
    public float ShootCoolTime
    {
        get
        {
            return fShootCoolTime;
        }
    }

    /// <summary>
    /// �ѱ� ������. ���� BulletCount ���� Max�� �����.
    /// </summary>
    public void Reload(int nBulletCount)
    {
        if (nBulletCount >= nBulletMaxCount)
        {
            nBulletNowCount = nBulletMaxCount;
        }

        else
        {
            nBulletNowCount = nBulletCount;
        }
    }

    /// <summary>
    /// ���� �Ѿ��� ���� ������ ���δ�. Fire ���� �� ����
    /// </summary>
    public void DecreaseBulletCount()
    {
        nBulletNowCount--;
    }
}