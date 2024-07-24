using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CAmmoCartidgeSlotSocketInteractor : XRSocketInteractor
{
    #region private º¯¼ö
    [SerializeField]
    CWeapon weapon;
    #endregion

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        CPlayerSoundManager.Instance.PlaySoundOneShot(weapon.SoundAmmoInSound);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        CPlayerSoundManager.Instance.PlaySoundOneShot(weapon.SoundAmmoOutSound);
    }
}
