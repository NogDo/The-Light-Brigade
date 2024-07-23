using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeapon
{
    GEWEHR = 1,
    STG44 = 5
}

[System.Serializable]
public class CWeapon : CInteractable
{
    #region protected 변수
    protected EWeapon weaponType;

    protected float fShootCoolTime;
    protected float fDamage;
    protected int nBulletMaxCount;
    protected int nBulletNowCount;
    #endregion

    /// <summary>
    /// Interaction이 가능한 물체가 가지고 있는 고유 ActionNumber의 값 (Hand Animation을 정의하기 위한것), 현재 사용되지 않음
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
    /// 현재 남은 총알의 개수
    /// </summary>
    public int BulletCount
    {
        get
        {
            return nBulletNowCount;
        }
    }

    /// <summary>
    /// 총의 데미지
    /// </summary>
    public float Damage
    {
        get
        {
            float randomDamage = 0.0f;

            switch (weaponType)
            {
                case EWeapon.GEWEHR:
                    randomDamage = Random.Range(fDamage - 2.5f, fDamage + 2.5f);
                    break;

                case EWeapon.STG44:
                    randomDamage = Random.Range(fDamage - 1.0f, fDamage + 1.0f);
                    break;
            }

            randomDamage = Mathf.Floor(randomDamage * 10.0f) / 10.0f;

            return randomDamage;
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
    /// 총 발사 쿨타임
    /// </summary>
    public float ShootCoolTime
    {
        get
        {
            return fShootCoolTime;
        }
    }

    /// <summary>
    /// 총기 재장전. 현재 BulletCount 수를 Max로 만든다.
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
    /// 현재 총알의 남은 개수를 줄인다. Fire 됐을 때 실행
    /// </summary>
    public void DecreaseBulletCount()
    {
        nBulletNowCount--;
    }
}