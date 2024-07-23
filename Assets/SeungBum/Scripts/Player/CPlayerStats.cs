using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerStats : MonoBehaviour
{
    #region private ����
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
    /// �÷��̾� �ִ� ü��
    /// </summary>
    public int MaxHP
    {
        get
        {
            return nMaxHP;
        }
    }

    /// <summary>
    /// �÷��̾� ���� ü��
    /// </summary>
    public int HP
    {
        get
        {
            return nHP;
        }
    }

    /// <summary>
    /// �÷��̾� ������
    /// </summary>
    public int Money
    {
        get
        {
            return nMoney;
        }
    }

    /// <summary>
    /// �÷��̾� ��ȥ
    /// </summary>
    public int Soul
    {
        get
        {
            return nSoul;
        }
    }

    /// <summary>
    /// �÷��̾� �ִ� ���
    /// </summary>
    public int MaxLife
    {
        get
        {
            return nMaxLife;
        }
    }

    /// <summary>
    /// �÷��̾� ���
    /// </summary>
    public int Life
    {
        get
        {
            return nLife;
        }
    }

    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public int Level
    {
        get
        {
            return nLevel;
        }
    }

    /// <summary>
    /// �÷��̾ ������ �� �ִ� �ִ� źâ ����
    /// </summary>
    public int MaxAmmo
    {
        get
        {
            return nMaxAmmoCount;
        }
    }

    /// <summary>
    /// �÷��̾� ���� źâ ����
    /// </summary>
    public int Ammo
    {
        get
        {
            return nAmmoCount;
        }
    }

    /// <summary>
    /// �÷��̾� ����ġ
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
    /// �÷��̾� ü���� �����Ѵ�.
    /// </summary>
    /// <param name="hp">������ ü��</param>
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
    /// �÷��̾� ����� �ϳ� ���ش�.
    /// </summary>
    public void DecreaseLife()
    {
        nLife--;
    }
}
