using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmmo : MonoBehaviour
{
    #region protected 변수
    [SerializeField]
    protected GameObject[] oBullets;
    [SerializeField]
    protected List<AudioClip> soundEffect;

    protected EWeapon equipWeaponType;
    protected int nBulletMaxCount;
    protected int nBulletNowCount;
    #endregion

    /// <summary>
    /// 탄창 잡는 소리
    /// </summary>
    public AudioClip SoundGrab
    {
        get
        {
            return soundEffect[0];
        }
    }

    /// <summary>
    /// 탄창 놓는 소리
    /// </summary>
    public AudioClip SoundRelease
    {
        get
        {
            return soundEffect[1];
        }
    }

    /// <summary>
    /// 현재 Ammo를 착용할 수 있는 Weapon의 타입
    /// </summary>
    public EWeapon EquipWeaponType
    {
        get
        {
            return equipWeaponType;
        }
    }

    /// <summary>
    /// 탄창의 최대 총알 개수
    /// </summary>
    public int BulletMaxCount
    {
        get
        {
            return nBulletMaxCount;
        }
    }

    /// <summary>
    /// 탄창의 남은 총알 개수
    /// </summary>
    public int BulletNowCount
    {
        get
        {
            return nBulletNowCount;
        }
    }

    /// <summary>
    /// 남은 총알의 비율
    /// </summary>
    public float RemainBulletPercent
    {
        get
        {
            return nBulletNowCount / (float)nBulletMaxCount;
        }
    }

    /// <summary>
    /// 현재 총알의 남은 개수를 줄인다.
    /// </summary>
    public void DecreaseBulltCount()
    {
        nBulletNowCount--;
        oBullets[nBulletNowCount].SetActive(false);
    }
}
