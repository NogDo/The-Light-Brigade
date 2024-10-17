using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeapon
{
    GEWEHR = 1,
    STG44 = 5
}

public class CWeapon : MonoBehaviour
{
    #region protected 변수
    [SerializeField]
    protected List<AudioClip> soundEffect;

    protected EWeapon weaponType;

    protected float fShootCoolTime;
    protected float fDamage;
    protected float fRecoilTime;
    #endregion

    /// <summary>
    /// 총의 데미지
    /// </summary>
    public float Damage
    {
        get
        {
            float randomDamage = 0.0f;

            switch (weaponType)
            {
                case EWeapon.GEWEHR:
                    randomDamage = Random.Range(fDamage - 2.5f, fDamage + 2.5f);
                    break;

                case EWeapon.STG44:
                    randomDamage = Random.Range(fDamage - 1.0f, fDamage + 1.0f);
                    break;
            }

            randomDamage = Mathf.Floor(randomDamage * 10.0f) / 10.0f;

            return randomDamage;
        }
    }

    /// <summary>
    /// Weapon의 종류
    /// </summary>
    public EWeapon WeaponType
    {
        get
        {
            return weaponType;
        }

        protected set
        {
            weaponType = value;
        }
    }

    /// <summary>
    /// 총 발사 쿨타임
    /// </summary>
    public float ShootCoolTime
    {
        get
        {
            return fShootCoolTime;
        }
    }

    /// <summary>
    /// 총기의 반동이 동작되는 시간
    /// </summary>
    public float RecoilTime
    {
        get
        {
            return fRecoilTime;
        }
    }

    /// <summary>
    /// 총 쏘는 소리
    /// </summary>
    public AudioClip SoundShot
    {
        get
        {
            return soundEffect[0];
        }
    }

    /// <summary>
    /// 총 Trigger Grab 소리
    /// </summary>
    public AudioClip SoundTriggerGrab
    {
        get
        {
            return soundEffect[1];
        }
    }

    /// <summary>
    /// 총 Barrel Grab 소리
    /// </summary>
    public AudioClip SoundBarrelGrab
    {
        get
        {
            return soundEffect[2];
        }
    }

    /// <summary>
    /// 총 Bolt Grab 소리
    /// </summary>
    public AudioClip SoundBoltGrab
    {
        get
        {
            return soundEffect[3];
        }
    }

    /// <summary>
    /// 장전 소리
    /// </summary>
    public AudioClip SoundReload
    {
        get
        {
            return soundEffect[4];
        }
    }

    /// <summary>
    /// 총알 없을 때 쏘는 소리
    /// </summary>
    public AudioClip SoundEmptyShot
    {
        get
        {
            return soundEffect[5];
        }
    }

    /// <summary>
    /// 탄창 In 소리
    /// </summary>
    public AudioClip SoundAmmoInSound
    {
        get
        {
            return soundEffect[6];
        }
    }

    /// <summary>
    /// 탄창 Out 소리
    /// </summary>
    public AudioClip SoundAmmoOutSound
    {
        get
        {
            return soundEffect[7];
        }
    }

    /// <summary>
    /// 총 놓는 소리(인벤토리 In이랑 똑같을듯?)
    /// </summary>
    public AudioClip SoundReleaseWeapon
    {
        get
        {
            return soundEffect[8];
        }
    }
}