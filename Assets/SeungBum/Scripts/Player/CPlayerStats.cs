using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerStats : MonoBehaviour
{
    #region private 변수
    int nMaxHP;
    int nHP;
    int nSoul;
    int nMaxLife;
    int nLife;
    int nMaxAmmoCount;
    int nAmmoCount;
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

    void Start()
    {
        nMaxHP = 20;
        nHP = 20;
        nSoul = 0;
        nMaxLife = 2;
        nLife = 2;
        nMaxAmmoCount = 10;
        nAmmoCount = 0;
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
    /// 플레이어 최대 체력을 변경한다.
    /// </summary>
    /// <param name="maxHp"></param>
    public void ChangeMaxHP(int maxHp)
    {
        nMaxHP = maxHp;
    }

    /// <summary>
    /// 플레이어 소울량을 변경한다
    /// </summary>
    /// <param name="soul"></param>
    public void ChangeSoul(int soul)
    {
        nSoul = soul;
    }

    /// <summary>
    /// 플레이어 목숨을 하나 없앤다.
    /// </summary>
    public void DecreaseLife()
    {
        nLife--;
    }
}
