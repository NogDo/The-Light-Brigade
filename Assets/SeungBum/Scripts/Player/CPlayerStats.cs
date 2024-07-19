using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerStats : MonoBehaviour
{
    #region private 변수
    int nHP;
    int nMoney;
    #endregion

    /// <summary>
    /// 플레이어 체력
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

    void Start()
    {
        nHP = 10;
        nMoney = 0;
    }

    /// <summary>
    /// 플레이어 체력을 변경한다.
    /// </summary>
    /// <param name="hp">변경할 체력</param>
    public void ChangeHP(int hp)
    {
        nHP = hp;
    }
}
