using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmoBoxBrokenPartInActiveController : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("InActiveBrokenPart", 2.0f);
    }

    /// <summary>
    /// 부서진 잔해를 비활성화하는 메서드
    /// </summary>
    void InActiveBrokenPart()
    {
        gameObject.SetActive(false);
    }
}