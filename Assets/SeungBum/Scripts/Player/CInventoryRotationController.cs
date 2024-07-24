using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventoryRotationController : MonoBehaviour
{
    #region private º¯¼ö
    [SerializeField]
    Transform tfMainCamera;

    #endregion

    void LateUpdate()
    {
        Quaternion cameraRotation = Quaternion.identity;
        cameraRotation.y = tfMainCamera.localRotation.y;

        transform.localRotation = cameraRotation;
    }
}