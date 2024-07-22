using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINodeRotationController : MonoBehaviour
{
    #region private º¯¼ö
    Transform tfTarget;
    #endregion

    void Awake()
    {
        tfTarget = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(tfTarget);
    }
}
