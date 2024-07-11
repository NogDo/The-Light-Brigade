using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CWeaponController : MonoBehaviour
{
    #region private 변수
    XRGrabInteractable grabInteractable;
    #endregion

    #region public 변수
    public Transform bulletTransform;
    #endregion

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.activated.AddListener(Fire);
    }

    void OnDisable()
    {
        if (grabInteractable is not null)
        {
            grabInteractable.activated.RemoveListener(Fire);
        }
    }

    public void Fire(ActivateEventArgs eventArgs)
    {
        RaycastHit hit;

        Debug.Log("Fire");

        if (Physics.Raycast(bulletTransform.position, bulletTransform.forward, out hit, float.MaxValue))
        {
            if (hit.transform.TryGetComponent<IHittable>(out IHittable hitObj))
            {
                hitObj.Hit();
            }
        }
    }
}
