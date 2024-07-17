using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class CBoltInteractable : XRGrabInteractable
{
    #region private º¯¼ö
    [SerializeField]
    CHandAnimationController animataedLeftHand;
    [SerializeField]
    CWeapon weapon;

    XRInputModalityManager inputModalityManager;
    XRDirectInteractor leftDirectController;
    XRRayInteractor leftRayController;
    CHandAnimationController leftControllerAnimation;
    #endregion

    void Start()
    {
        inputModalityManager = FindObjectOfType<XRInputModalityManager>();

        leftDirectController = inputModalityManager.leftController.GetComponentInChildren<XRDirectInteractor>();
        leftRayController = inputModalityManager.leftController.GetComponentInChildren<XRRayInteractor>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        int weaponNumber = 0;

        switch (weapon.WeaponType)
        {
            case EWeapon.GEWEHR:
                weaponNumber = 1;
                break;
        }

        if (args.interactorObject as XRDirectInteractor == leftDirectController || args.interactorObject as XRRayInteractor == leftRayController)
        {
            animataedLeftHand.gameObject.SetActive(true);
            animataedLeftHand.ActionAnimation(weaponNumber + (int)EGrabPoint.BOLT);

            if (leftControllerAnimation is null)
            {
                leftControllerAnimation = inputModalityManager.leftController.GetComponentInChildren<CHandAnimationController>();
            }
            leftControllerAnimation.tfHandOffsetNode.gameObject.SetActive(false);
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        if (args.interactorObject as XRDirectInteractor == leftDirectController || args.interactorObject as XRRayInteractor == leftRayController)
        {
            animataedLeftHand.ActionAnimation(0);
            animataedLeftHand.gameObject.SetActive(false);

            leftControllerAnimation.tfHandOffsetNode.gameObject.SetActive(true);
        }

        base.OnSelectExiting(args);
    }
}