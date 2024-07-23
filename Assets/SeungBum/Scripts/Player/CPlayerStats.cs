using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerStats : MonoBehaviour
{
    #region private 변수
    int nMaxHP;
    int nHP;
    int nMoney;
    int nSoul;
    int nMaxLife;
    int nLife;
    int nMaxAmmoCount;
    int nAmmoCount;
    int nLevel;
    float fExp;
    #endregion

    /// <summary>
    /// 플레이어 최대 체력
    /// </summary>
    public int MaxHP
    {
        get
        {
            return nMaxHP;
        }
    }

    /// <summary>
    /// 플레이어 현재 체력
    /// </summary>
    public int HP
    {
        get
        {
            return nHP;
        }
    }

    /// <summary>
    /// 플레이어 소지금
    /// </summary>
    public int Money
    {
        get
        {
            return nMoney;
        }
    }

    /// <summary>
    /// 플레이어 영혼
    /// </summary>
    public int Soul
    {
        get
        {
            return nSoul;
        }
    }

    /// <summary>
    /// 플레이어 최대 목숨
    /// </summary>
    public int MaxLife
    {
        get
        {
            return nMaxLife;
        }
    }

    /// <summary>
    /// 플레이어 목숨
    /// </summary>
    public int Life
    {
        get
        {
            return nLife;
        }
    }

    /// <summary>
    /// 플레이어 레벨
    /// </summary>
    public int Level
    {
        get
        {
            return nLevel;
        }
    }

    /// <summary>
    /// 플레이어가 보유할 수 있는 최대 탄창 개수
    /// </summary>
    public int MaxAmmo
    {
        get
        {
            return nMaxAmmoCount;
        }
    }

    /// <summary>
    /// 플레이어 보유 탄창 개수
    /// </summary>
    public int Ammo
    {
        get
        {
            return nAmmoCount;
        }
    }

    /// <summary>
    /// 플레이어 경험치
    /// </summary>
    public float EXP
    {
        get
        {
            return fExp;
        }
    }

    void Start()
    {
        nMaxHP = 20;
        nHP = 20;
        nMoney = 0;
        nSoul = 0;
        nMaxLife = 2;
        nLife = 2;
        nMaxAmmoCount = 10;
        nAmmoCount = 0;
        nLevel = 0;
        fExp = 0.0f;
    }

    /// <summary>
    /// 플레이어 체력을 변경한다.
    /// </summary>
    /// <param name="hp">변경할 체력</param>
    public void ChangeHP(int hp)
    {
        if (hp > nMaxHP)
        {
            hp = nMaxHP;
        }

        else if (hp < 0)
        {
            hp = 0;
        }

        nHP = hp;
    }

    /// <summary>
    /// 플레이어 목숨을 하나 없앤다.
    /// </summary>
    public void DecreaseLife()
    {
        nLife--;
    }
}
