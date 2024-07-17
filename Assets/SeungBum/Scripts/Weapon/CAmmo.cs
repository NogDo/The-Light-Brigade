using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmo : MonoBehaviour
{
    #region protected ����
    protected EWeapon equipWeaponType;
    protected int nBulletCount;
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
    /// źâ�� ���� �Ѿ� ����
    /// </summary>
    public int BulletCount
    {
        get
        {
            return nBulletCount;
        }
    }

    /// <summary>
    /// ���� �Ѿ��� ���� ������ ���δ�.
    /// </summary>
    public void DecreaseBulltCount()
    {
        nBulletCount--;
    }
}
