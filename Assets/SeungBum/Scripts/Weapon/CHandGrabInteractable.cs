using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class CHandGrabInteractable : XRGrabInteractable
{
    #region private º¯¼ö
    [SerializeField]
    Transform tfBarrelHandPose;
    [SerializeField]
    Transform tfBoltHandPose;

    [SerializeField]
    CHandAnimationController animataedLeftHand;
    [SerializeField]
    CHandAnimationController animataedRightHand;

    XRInputModalityManager inputModalityManager;

    XRDirectInteractor leftController;
    XRDirectInteractor rightController;

    CHandAnimationController leftControllerAnimation;
    CHandAnimationController rightControllerAnimation;
    #endregion

    private void Start()
    {
        inputModalityManager = FindObjectOfType<XRInputModalityManager>();

        leftController = inputModalityManager.leftController.GetComponentInChildren<XRDirectInteractor>();
        rightController = inputModalityManager.rightController.GetComponentInChildren<XRDirectInteractor>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactorObject as XRDirectInteractor == leftController)
        {
            animataedLeftHand.gameObject.SetActive(true);
            if (args.interactableObject.transform.GetComponent<CWeapon>().ActionNumber == 3)
            {
                GetComponent<XRGeneralGrabTransformer>().allowTwoHandedRotation = XRGeneralGrabTransformer.TwoHandedRotationMode.FirstHandOnly;
                animataedLeftHand.transform.SetParent(tfBoltHandPose);
                animataedLeftHand.transform.localPosition = Vector3.zero;
                animataedLeftHand.transform.localRotation = Quaternion.identity;
            }

            else
            {
                GetComponent<XRGeneralGrabTransformer>().allowTwoHandedRotation = XRGeneralGrabTransformer.TwoHandedRotationMode.FirstHandDirectedTowardsSecondHand;
                animataedLeftHand.transform.SetParent(tfBarrelHandPose);
                animataedLeftHand.transform.localPosition = Vector3.zero;
                animataedLeftHand.transform.localRotation = Quaternion.identity;
            }
            animataedLeftHand.ActionAnimation(args.interactableObject.transform.GetComponent<CWeapon>().ActionNumber);

            if (leftControllerAnimation is null)
            {
                leftControllerAnimation = inputModalityManager.leftController.GetComponentInChildren<CHandAnimationController>();
            }
            leftControllerAnimation.tfHandOffsetNode.gameObject.SetActive(false);
        }

        else
        {
            animataedRightHand.gameObject.SetActive(true);
            animataedRightHand.ActionAnimation(args.interactableObject.transform.GetComponent<CWeapon>().ActionNumber);

            if (rightControllerAnimation is null)
            {
                rightControllerAnimation = inputModalityManager.rightController.GetComponentInChildren<CHandAnimationController>();
            }
            rightControllerAnimation.tfHandOffsetNode.gameObject.SetActive(false);
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        if (args.interactorObject as XRDirectInteractor == leftController)
        {
            animataedLeftHand.ActionAnimation(0);
            animataedLeftHand.gameObject.SetActive(false);

            leftControllerAnimation.tfHandOffsetNode.gameObject.SetActive(true);
        }

        else
        {
            animataedRightHand.ActionAnimation(0);
            animataedRightHand.gameObject.SetActive(false);

            rightControllerAnimation.tfHandOffsetNode.gameObject.SetActive(true);
        }

        base.OnSelectExiting(args);
    }
}