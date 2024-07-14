using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum EGrabPoint
{
    TRIGGER = 1,
    BARREL,
    BOLT
}

public class CWeaponActionNumberChangeControl : MonoBehaviour
{
    #region public º¯¼ö
    public CWeapon weapon;
    public Transform tfHandPose;

    public EGrabPoint grabPointType;
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (weapon.GrabCount > 0)
            {
                weapon.GetComponent<XRGrabInteractable>().secondaryAttachTransform = tfHandPose;
                weapon.ActionNumber = (int)weapon.WeaponType * (int)grabPointType;
            }

            else
            {
                weapon.ActionNumber = (int)weapon.WeaponType;
            }
        }
    }
}