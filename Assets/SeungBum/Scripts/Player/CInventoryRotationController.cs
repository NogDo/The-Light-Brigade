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
        Quaternion cameraRotation = tfMainCamera.rotation;
        cameraRotation.x = 0.0f;
        cameraRotation.z = 0.0f;

        transform.rotation = cameraRotation;
    }
}