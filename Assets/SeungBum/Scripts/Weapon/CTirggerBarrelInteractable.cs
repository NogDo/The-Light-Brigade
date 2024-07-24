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
    [SerializeField]
    CBoltInteractable boltInteractable;
    [SerializeField]
    GameObject oNode;

    XRInputModalityManager inputModalityManager;

    XRDirectInteractor leftDirectController;
    XRDirectInteractor rightDirectController;
    XRRayInteractor leftRayController;
    XRRayInteractor rightRayController;

    CHandAnimationController leftControllerAnimation;
    CHandAnimationController rightControllerAnimation;

    bool isLeftGrab;
    bool isRightGrab;
    #endregion

    void Start()
    {
        inputModalityManager = FindObjectOfType<XRInputModalityManager>();

        leftDirectController = inputModalityManager.leftController.GetComponentInChildren<XRDirectInteractor>();
        rightDirectController = inputModalityManager.rightController.GetComponentInChildren<XRDirectInteractor>();
        leftRayController = inputModalityManager.leftController.GetComponentInChildren<XRRayInteractor>();
        rightRayController = inputModalityManager.rightController.GetComponentInChildren<XRRayInteractor>();

        isLeftGrab = false;
        isRightGrab = false;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        int weaponNumber = 0;

        switch (args.interactableObject.transform.GetComponent<CWeapon>().WeaponType)
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
            animataedLeftHand.gameObject.SetActive(true);
            animataedLeftHand.ActionAnimation(weaponNumber + (int)EGrabPoint.BARREL);
            CPlayerSoundManager.Instance.PlaySoundOneShot(args.interactableObject.transform.GetComponent<CWeapon>().SoundBarrelGrab);

            if (leftControllerAnimation is null)
            {
                leftControllerAnimation = inputModalityManager.leftController.GetComponentInChildren<CHandAnimationController>();
            }
            leftControllerAnimation.tfHandOffsetNode.gameObject.SetActive(false);

            args.interactableObject.transform.GetComponent<CWeaponController>().GrabLeftController(args);
            args.interactorObject.transform.root.GetComponentInChildren<UIPlayerStatsActiveController>().DontActive();

            oNode.SetActive(false);
            isLeftGrab = true;
        }

        else if (args.interactorObject as XRDirectInteractor == rightDirectController || args.interactorObject as XRRayInteractor == rightRayController)
        {
            animataedRightHand.gameObject.SetActive(true);
            animataedRightHand.ActionAnimation(weaponNumber + (int)EGrabPoint.TRIGGER);
            CPlayerSoundManager.Instance.PlaySoundOneShot(args.interactableObject.transform.GetComponent<CWeapon>().SoundTriggerGrab);

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

            oNode.SetActive(false);
            boltInteractable.enabled = true;
            isRightGrab = true;
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
            args.interactorObject.transform.root.GetComponentInChildren<UIPlayerStatsActiveController>().CanActive();

            isLeftGrab = false;
        }

        else if (args.interactorObject as XRDirectInteractor == rightDirectController || args.interactorObject as XRRayInteractor == rightRayController)
        {
            animataedRightHand.ActionAnimation(0);
            animataedRightHand.gameObject.SetActive(false);
            CPlayerSoundManager.Instance.PlaySoundOneShot(args.interactableObject.transform.GetComponent<CWeapon>().SoundReleaseWeapon);

            rightControllerAnimation.tfHandOffsetNode.gameObject.SetActive(true);

            args.interactableObject.transform.GetComponent<CWeaponController>().ReleaseRightController();
            args.interactableObject.transform.GetComponent<CWeaponController>().WeaponUI.gameObject.SetActive(false);
            args.interactorObject.transform.root.GetComponentInChildren<CPlayerController>().SetWeaponUI(null);

            boltInteractable.enabled = false;
            isRightGrab = false;
        }

        base.OnSelectExiting(args);
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        if (isLeftGrab || isRightGrab)
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