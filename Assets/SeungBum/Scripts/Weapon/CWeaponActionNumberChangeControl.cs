using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum EGrabPoint
{
    TRIGGER,
    BARREL,
    BOLT
}

public class CWeaponActionNumberChangeControl : MonoBehaviour
{
    #region public ����
    public CWeapon weapon;
    public Transform tfHandPose;

    public EGrabPoint grabPointType;
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (weapon.GetComponent<CHandGrabInteractable>().interactorsSelecting.Count > 0)
            {
                if (grabPointType != EGrabPoint.TRIGGER)
                {
                    //weapon.GetComponent<XRGrabInteractable>().secondaryAttachTransform = tfHandPose;
                    weapon.ActionNumber = (int)weapon.WeaponType + (int)grabPointType;
                }
            }

            else
            {
                weapon.ActionNumber = (int)weapon.WeaponType;
            }
        }
    }
}