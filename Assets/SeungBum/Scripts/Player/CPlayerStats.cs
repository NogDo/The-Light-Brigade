using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerStats : MonoBehaviour
{
    #region private ����
    int nHP;
    int nMoney;
    #endregion

    /// <summary>
    /// �÷��̾� ü��
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

    void Start()
    {
        nHP = 10;
        nMoney = 0;
    }

    /// <summary>
    /// �÷��̾� ü���� �����Ѵ�.
    /// </summary>
    /// <param name="hp">������ ü��</param>
    public void ChangeHP(int hp)
    {
        nHP = hp;
    }
}
