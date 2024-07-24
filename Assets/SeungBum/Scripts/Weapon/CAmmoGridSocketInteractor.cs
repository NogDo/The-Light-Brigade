using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class CAmmoGridSocketInteractor : XRGridSocketInteractor
{
    #region private º¯¼ö
    [SerializeField]
    Transform tfAmmos;

    UIPlayerStats playerStats;

    int nAmmoCount;
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();

        nAmmoCount = 0;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        args.interactableObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //args.interactableObject.transform.SetParent(tfAmmos);
        nAmmoCount++;

        if (playerStats is null)
        {
            playerStats = transform.root.GetComponentInChildren<CPlayerController>().PlayerStatsUI;
        }
        playerStats.ChangeAmmoText(nAmmoCount);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        args.interactableObject.transform.SetParent(tfAmmos);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        args.interactableObject.transform.localScale = Vector3.one;
        args.interactableObject.transform.SetParent(null);
        nAmmoCount--;

        if (playerStats is null)
        {
            playerStats = transform.root.GetComponentInChildren<CPlayerController>().PlayerStatsUI;
        }
        playerStats.ChangeAmmoText(nAmmoCount);
    }
}