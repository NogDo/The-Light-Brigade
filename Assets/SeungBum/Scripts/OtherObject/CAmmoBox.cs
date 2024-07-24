using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmoBox : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    List<AudioClip> soundEffect;

    int nActionNumber;
    #endregion

    /// <summary>
    /// 탄창 박스 그랩 사운드
    /// </summary>
    public AudioClip SoundGrab
    {
        get
        {
            return soundEffect[0];
        }
    }

    /// <summary>
    /// 탄창 박스 부서지는 사운드
    /// </summary>
    public AudioClip SoundBreak
    {
        get
        {
            return soundEffect[1];
        }
    }

    /// <summary>
    /// AmmoBox가 재생항 Hand Animation의 Number값
    /// </summary>
    public int ActionNumber
    {
        get
        {
            return nActionNumber;
        }
    }

    void Start()
    {
        nActionNumber = 99;
    }
}