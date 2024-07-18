using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class CTirggerBarrelInteractable : XRGrabInteractable
{
    #region private º¯¼ö
    [SerializeField]
    CHandAnimationController animataedLeftHand;
    [SerializeField]
    CHandAnimationController animataedRightHand;

    XRInputModalityManager inputModalityManager;

    XRDirectInteractor leftDirectController;
    XRDirectInteractor rightDirectController;
    XRRayInteractor leftRayController;
    XRRayInteractor rightRayController;

    CHandAnimationController leftControllerAnimation;
    CHandAnimationController rightControllerAnimation;
    #endregion

    void Start()
    {
        inputModalityManager = FindObjectOfType<XRInputModalityManager>();

        leftDirectController = inputModalityManager.leftController.GetComponentInChildren<XRDirectInteractor>();
        rightDirectController = inputModalityManager.rightController.GetComponentInChildren<XRDirectInteractor>();
        leftRayController = inputModalityManager.leftController.GetComponentInChildren<XRRayInteractor>();
        rightRayController = inputModalityManager.rightController.GetComponentInChildren<XRRayInteractor>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        int weaponNumber = 0;

        switch (args.interactableObject.transform.GetComponent<CWeapon>().WeaponType)
        {
            case EWeapon.GEWEHR:
                weaponNumber = 1;
                break;
        }

        if (args.interactorObject as XRDirectInteractor == leftDirectController || args.interactorObject as XRRayInteractor == leftRayController)
        {
            animataedLeftHand.gameObject.SetActive(true);
            animataedLeftHand.ActionAnimation(weaponNumber + (int)EGrabPoint.BARREL);

            if (leftControllerAnimation is null)
            {
                leftControllerAnimation = inputModalityManager.leftController.GetComponentInChildren<CHandAnimationController>();
            }
            leftControllerAnimation.tfHandOffsetNode.gameObject.SetActive(false);

            args.interactableObject.transform.GetComponent<CWeaponController>().GrabLeftController(args);
        }

        else
        {
            animataedRightHand.gameObject.SetActive(true);
            animataedRightHand.ActionAnimation(weaponNumber + (int)EGrabPoint.TRIGGER);

            if (rightControllerAnimation is null)
            {
                rightControllerAnimation = inputModalityManager.rightController.GetComponentInChildren<CHandAnimationController>();
            }
            rightControllerAnimation.tfHandOffsetNode.gameObject.SetActive(false);

            args.interactableObject.transform.GetComponent<CWeaponController>().GrabRightController(args);
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

            args.interactableObject.transform.GetComponent<CWeaponController>().ReleaseLeftController();
        }

        else
        {
            animataedRightHand.ActionAnimation(0);
            animataedRightHand.gameObject.SetActive(false);

            rightControllerAnimation.tfHandOffsetNode.gameObject.SetActive(true);

            args.interactableObject.transform.GetComponent<CWeaponController>().ReleaseRightController();
        }

        base.OnSelectExiting(args);
    }
}