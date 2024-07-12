using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInteractable : MonoBehaviour
{
    #region protected 변수
    protected int nActionNumber;
    #endregion

    /// <summary>
    /// Interaction이 가능한 물체가 가지고 있는 고유 ActionNumber의 값 (Hand Animation을 정의하기 위한것)
    /// </summary>
    public virtual int ActionNumber
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
}
