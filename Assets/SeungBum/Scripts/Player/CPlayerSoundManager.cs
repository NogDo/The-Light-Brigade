using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSoundManager : MonoBehaviour
{
    #region private 변수
    static CPlayerSoundManager instance;
    AudioSource audioSource;
    #endregion

    public static CPlayerSoundManager Instance
    {
        get
        {
            if (instance is null)
            {
                instance = FindObjectOfType<CPlayerSoundManager>();

                if (instance is null)
                {
                    instance = new CPlayerSoundManager();
                    DontDestroyOnLoad(instance);
                }
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance is null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(this);
        }

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 효과음을 재생하는 메서드
    /// </summary>
    /// <param name="audioClip">재생할 효과음</param>
    public void PlaySoundOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
