using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSoundManager : MonoBehaviour
{
    #region private º¯¼ö
    AudioSource audioSource;
    #endregion

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
