using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmoBox : MonoBehaviour
{
    #region private ����
    int nActionNumber;
    #endregion

    /// <summary>
    /// AmmoBox�� ����� Hand Animation�� Number��
    /// </summary>
    public int ActionNumber
    {
        get
        {
            return nActionNumber;
        }
    }

    void Start()
    {
        nActionNumber = 99;
    }
}