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
        base.OnSelectEntering(args);

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
            args.interactorObject.transform.root.GetComponentInChildren<UIPlayerStatsActiveController>().DontActive();
        }

        else if (args.interactorObject as XRDirectInteractor == rightDirectController || args.interactorObject as XRRayInteractor == rightRayController)
        {
            animataedRightHand.gameObject.SetActive(true);
            animataedRightHand.ActionAnimation(weaponNumber + (int)EGrabPoint.TRIGGER);

            if (rightControllerAnimation is null)
            {
                rightControllerAnimation = inputModalityManager.rightController.GetComponentInChildren<CHandAnimationController>();
            }
            rightControllerAnimation.tfHandOffsetNode.gameObject.SetActive(false);

            args.interactableObject.transform.GetComponent<CWeaponController>().GrabRightController(args);
            args.interactorObject.transform.root.GetComponentInChildren<CPlayerController>().SetWeaponUI
                (
                    args.interactableObject.transform.GetComponent<CWeaponController>().WeaponUI
                );
        }
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        if (args.interactorObject as XRDirectInteractor == leftDirectController || args.interactorObject as XRRayInteractor == leftRayController)
        {
            animataedLeftHand.ActionAnimation(0);
            animataedLeftHand.gameObject.SetActive(false);

            leftControllerAnimation.tfHandOffsetNode.gameObject.SetActive(true);

            args.interactableObject.transform.GetComponent<CWeaponController>().ReleaseLeftController();
            args.interactorObject.transform.root.GetComponentInChildren<UIPlayerStatsActiveController>().CanActive();
        }

        else if (args.interactorObject as XRDirectInteractor == rightDirectController || args.interactorObject as XRRayInteractor == rightRayController)
        {
            animataedRightHand.ActionAnimation(0);
            animataedRightHand.gameObject.SetActive(false);

            rightControllerAnimation.tfHandOffsetNode.gameObject.SetActive(true);

            args.interactableObject.transform.GetComponent<CWeaponController>().ReleaseRightController();
            args.interactableObject.transform.GetComponent<CWeaponController>().WeaponUI.gameObject.SetActive(false);
            args.interactorObject.transform.root.GetComponentInChildren<CPlayerController>().SetWeaponUI(null);
        }
    }
}