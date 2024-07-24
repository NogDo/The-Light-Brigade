 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class CAmmoInteractable : XRGrabInteractable
{
    #region private º¯¼ö
    [SerializeField]
    GameObject oNode;

    XRInputModalityManager inputModalityManager;

    XRDirectInteractor leftDirectController;
    XRDirectInteractor rightDirectController;
    XRRayInteractor leftRayController;
    XRRayInteractor rightRayController;

    CHandAnimationController leftHandAnimationController;
    CHandAnimationController rightHandAnimationController;

    bool isGrab = false;
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();

        inputModalityManager = FindObjectOfType<XRInputModalityManager>();

        leftDirectController = inputModalityManager.leftController.GetComponentInChildren<XRDirectInteractor>();
        rightDirectController = inputModalityManager.rightController.GetComponentInChildren<XRDirectInteractor>();
        leftRayController = inputModalityManager.leftController.GetComponentInChildren<XRRayInteractor>();
        rightRayController = inputModalityManager.rightController.GetComponentInChildren<XRRayInteractor>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        int weaponNumber = 0;

        switch (args.interactableObject.transform.GetComponent<CAmmo>().EquipWeaponType)
        {
            case EWeapon.GEWEHR:
                weaponNumber = 1;
                break;

            case EWeapon.STG44:
                weaponNumber = 5;
                break;
        }

        if (args.interactorObject as XRDirectInteractor == leftDirectController || args.interactorObject as XRRayInteractor == leftRayController)
        {
            if (leftHandAnimationController is null)
            {
                leftHandAnimationController = inputModalityManager.leftController.GetComponentInChildren<CHandAnimationController>();
            }

            CPlayerSoundManager.Instance.PlaySoundOneShot(GetComponent<CAmmo>().SoundGrab);
            leftHandAnimationController.ActionAnimation(weaponNumber + (int)EGrabPoint.AMMO);
            oNode.SetActive(false);
            isGrab = true;
        }

        else if (args.interactorObject as XRDirectInteractor == rightDirectController || args.interactorObject as XRRayInteractor == rightRayController)
        {
            if (rightHandAnimationController is null)
            {
                rightHandAnimationController = inputModalityManager.rightController.GetComponentInChildren<CHandAnimationController>();
            }

            CPlayerSoundManager.Instance.PlaySoundOneShot(GetComponent<CAmmo>().SoundGrab);
            rightHandAnimationController.ActionAnimation(weaponNumber + (int)EGrabPoint.AMMO);
            oNode.SetActive(false);
            isGrab = true;
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        if (args.interactorObject as XRDirectInteractor == leftDirectController || args.interactorObject as XRRayInteractor == leftRayController)
        {
            CPlayerSoundManager.Instance.PlaySoundOneShot(GetComponent<CAmmo>().SoundRelease);
            leftHandAnimationController.ActionAnimation(0);
            isGrab = false;
        }

        else if (args.interactorObject as XRDirectInteractor == rightDirectController || args.interactorObject as XRRayInteractor == rightRayController)
        {
            CPlayerSoundManager.Instance.PlaySoundOneShot(GetComponent<CAmmo>().SoundRelease);
            rightHandAnimationController.ActionAnimation(0);
            isGrab = false;
        }

        base.OnSelectExiting(args);
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        if (isGrab)
        {
            return;
        }

        oNode.SetActive(true);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        oNode.SetActive(false);
    }
}