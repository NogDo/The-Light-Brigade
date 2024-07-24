using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class CItemPotionInteractable : XRGrabInteractable
{
    #region public º¯¼ö
    public GameObject oParticle;
    public AudioClip soundEffect;
    #endregion

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        CPlayerSoundManager.Instance.PlaySoundOneShot(soundEffect);

        args.interactorObject.transform.GetComponentInParent<CPlayerController>().Heal(5);

        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);
        Instantiate(oParticle, spawnPoint, transform.rotation);

        gameObject.SetActive(false);
    }
}