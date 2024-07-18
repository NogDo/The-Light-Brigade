using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmo : MonoBehaviour
{
    #region protected ����
    protected EWeapon equipWeaponType;
    protected int nBulletMaxCount;
    protected int nBulletNowCount;
    #endregion

    /// <summary>
    /// ���� Ammo�� ������ �� �ִ� Weapon�� Ÿ��
    /// </summary>
    public EWeapon EquipWeaponType
    {
        get
        {
            return equipWeaponType;
        }
    }

    /// <summary>
    /// źâ�� �ִ� �Ѿ� ����
    /// </summary>
    public int BulletMaxCount
    {
        get
        {
            return nBulletMaxCount;
        }
    }

    /// <summary>
    /// źâ�� ���� �Ѿ� ����
    /// </summary>
    public int BulletNowCount
    {
        get
        {
            return nBulletNowCount;
        }
    }

    /// <summary>
    /// ���� �Ѿ��� ����
    /// </summary>
    public float RemainBulletPercent
    {
        get
        {
            return nBulletNowCount / (float)nBulletMaxCount;
        }
    }

    /// <summary>
    /// ���� �Ѿ��� ���� ������ ���δ�.
    /// </summary>
    public void DecreaseBulltCount()
    {
        nBulletNowCount--;
    }
}
