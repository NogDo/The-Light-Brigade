using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class CAmmoGridSocketInteractor : XRGridSocketInteractor
{
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        args.interactableObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        args.interactableObject.transform.localScale = Vector3.one;

        base.OnSelectExiting(args);
    }
}