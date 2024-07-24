using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class CAmmoGridSocketInteractor : XRGridSocketInteractor
{
    #region private º¯¼ö
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
        args.interactableObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        nAmmoCount++;

        if (playerStats is null)
        {
            playerStats = transform.root.GetComponentInChildren<CPlayerController>().PlayerStatsUI;
        }
        playerStats.ChangeAmmoText(nAmmoCount);

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        args.interactableObject.transform.localScale = Vector3.one;
        nAmmoCount--;

        if (playerStats is null)
        {
            playerStats = transform.root.GetComponentInChildren<CPlayerController>().PlayerStatsUI;
        }
        playerStats.ChangeAmmoText(nAmmoCount);

        base.OnSelectExiting(args);
    }
}