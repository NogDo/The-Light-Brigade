using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CWeaponController : MonoBehaviour
{
    #region private º¯¼ö
    XRGrabInteractable grabInteractable;
    #endregion

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.activated.AddListener(Fire);
    }

    public void Fire(ActivateEventArgs eventArgs)
    {
        Debug.Log("Fire");
    }
}
