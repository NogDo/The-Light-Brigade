using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class CAmmoBoxInteractable : XRGrabInteractable
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

    private void Start()
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

        if (args.interactorObject as XRDirectInteractor == leftDirectController || args.interactorObject as XRRayInteractor == leftRayController)
        {
            if (leftHandAnimationController is null)
            {
                leftHandAnimationController = inputModalityManager.leftController.GetComponentInChildren<CHandAnimationController>();
            }

            CPlayerSoundManager.Instance.PlaySoundOneShot(GetComponent<CAmmoBox>().SoundGrab);
            leftHandAnimationController.ActionAnimation(args.interactableObject.transform.GetComponent<CAmmoBox>().ActionNumber);
            oNode.SetActive(false);
            isGrab = true;
        }

        else if (args.interactorObject as XRDirectInteractor == rightDirectController || args.interactorObject as XRRayInteractor == rightRayController)
        {
            if (rightHandAnimationController is null)
            {
                rightHandAnimationController = inputModalityManager.rightController.GetComponentInChildren<CHandAnimationController>();
            }

            CPlayerSoundManager.Instance.PlaySoundOneShot(GetComponent<CAmmoBox>().SoundGrab);
            rightHandAnimationController.ActionAnimation(args.interactableObject.transform.GetComponent<CAmmoBox>().ActionNumber);
            oNode.SetActive(false);
            isGrab = true;
        }
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        if (args.interactorObject as XRDirectInteractor == leftDirectController || args.interactorObject as XRRayInteractor == leftRayController)
        {
            leftHandAnimationController.ActionAnimation(0);
            isGrab = false;
        }

        else if (args.interactorObject as XRDirectInteractor == rightDirectController || args.interactorObject as XRRayInteractor == rightRayController)
        {
            rightHandAnimationController.ActionAnimation(0);
            isGrab = false;
        }
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