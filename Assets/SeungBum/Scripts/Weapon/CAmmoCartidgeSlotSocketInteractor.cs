using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CAmmoCartidgeSlotSocketInteractor : XRSocketInteractor
{
    #region private º¯¼ö
    CPlayerSoundManager playerSoundManager;

    [SerializeField]
    CWeapon weapon;
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();

        playerSoundManager = FindObjectOfType<CPlayerSoundManager>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        playerSoundManager.PlaySoundOneShot(weapon.SoundAmmoInSound);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        playerSoundManager.PlaySoundOneShot(weapon.SoundAmmoOutSound);
    }
}
