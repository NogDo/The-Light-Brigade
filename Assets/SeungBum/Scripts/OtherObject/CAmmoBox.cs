using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmoBox : MonoBehaviour
{
    #region private 변수
    int nActionNumber;
    #endregion

    /// <summary>
    /// AmmoBox가 재생항 Hand Animation의 Number값
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