using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventoryRotationController : MonoBehaviour
{
    #region private ����
    [SerializeField]
    Transform tfMainCamera;

    #endregion

    void LateUpdate()
    {
        Quaternion cameraRotation = Quaternion.identity;
        cameraRotation.y = tfMainCamera.rotation.y;

        transform.rotation = cameraRotation;
    }
}
