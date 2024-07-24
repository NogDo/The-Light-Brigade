using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CItemHPInteractable : XRGrabInteractable
{
    #region public º¯¼ö
    public GameObject oParticle;
    public AudioClip soundEffect;
    #endregion

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        CPlayerSoundManager.Instance.PlaySoundOneShot(soundEffect);

        args.interactorObject.transform.GetComponentInParent<CPlayerController>().IncreaseMaxHP(5);

        Instantiate(oParticle, transform.position, transform.rotation);

        gameObject.SetActive(false);
    }
}