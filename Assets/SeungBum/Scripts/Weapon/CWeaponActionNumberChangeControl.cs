using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum EGrabPoint
{
    TRIGGER,
    BARREL,
    BOLT,
    AMMO
}

public class CWeaponActionNumberChangeControl : MonoBehaviour
{
    #region public º¯¼ö
    public CWeapon weapon;
    public Transform tfHandPose;

    public EGrabPoint grabPointType;
    #endregion
}