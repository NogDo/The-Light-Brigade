using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CWeaponController : MonoBehaviour
{
    #region private 변수
    XRGrabInteractable grabInteractable;
    CWeapon weapon;
    CAmmo nowEquipAmmo;

    ActionBasedController leftController;
    ActionBasedController rightController;

    CHitExplosionParticlePool hitExplosionParticlePool;

    [SerializeField]
    UIWeapon weaponUI;

    Vector3 modelStartPosition;
    bool isFireReady;
    #endregion

    #region public 변수
    public Transform bulletTransform;
    public Transform modelTransform;
    public GameObject oMuzzleParticle;
    public XRSocketInteractor ammoSoketInteractor;
    #endregion

    void OnEnable()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        hitExplosionParticlePool = FindObjectOfType<CHitExplosionParticlePool>();
        weapon = GetComponent<CWeapon>();
        nowEquipAmmo = null;

        grabInteractable.activated.AddListener(Fire);

        ammoSoketInteractor.selectEntered.AddListener(AddAmmo);
        ammoSoketInteractor.selectExited.AddListener(RemoveAmmo);

        isFireReady = true;

        modelStartPosition = modelTransform.localPosition;
    }

    void OnDisable()
    {
        if (grabInteractable is not null)
        {
            grabInteractable.activated.RemoveListener(Fire);
        }

        if (ammoSoketInteractor is not null)
        {
            ammoSoketInteractor.selectEntered.RemoveListener(AddAmmo);
            ammoSoketInteractor.selectExited.RemoveListener(RemoveAmmo);
        }
    }

    /// <summary>
    /// WeaponUI Class 반환
    /// </summary>
    public UIWeapon WeaponUI
    {
        get
        {
            return weaponUI;
        }
    }

    /// <summary>
    /// 무기를 왼쪽 손으로 그랩했을 때 leftController를 할당
    /// </summary>
    /// <param name="args"></param>
    public void GrabLeftController(SelectEnterEventArgs args)
    {
        leftController = args.interactorObject.transform.parent.GetComponent<ActionBasedController>();
    }

    /// <summary>
    /// 무기를 오른쪽 손으로 그랩했을 때 rightController를 할당
    /// </summary>
    /// <param name="args"></param>
    public void GrabRightController(SelectEnterEventArgs args)
    {
        rightController = args.interactorObject.transform.parent.GetComponent<ActionBasedController>();
    }

    /// <summary>
    /// 왼쪽 손의 그랩을 Release했을 때 leftController를 해제
    /// </summary>
    public void ReleaseLeftController()
    {
        leftController = null;
    }

    /// <summary>
    /// 오른쪽 손의 그랩을 Release했을 때 rightController를 해제
    /// </summary>
    public void ReleaseRightController()
    {
        rightController = null;
    }

    /// <summary>
    /// 총알을 발사하기위한 메서드
    /// </summary>
    /// <param name="eventArgs">ActiveEvent</param>
    public void Fire(ActivateEventArgs eventArgs)
    {
        if (!isFireReady)
        {
            return;
        }

        if (nowEquipAmmo is not null && nowEquipAmmo.BulletNowCount > 0)
        {
            CPlayerSoundManager.Instance.PlaySoundOneShot(weapon.SoundShot);
            nowEquipAmmo.DecreaseBulltCount();

            weaponUI.ChangeBulletCount(nowEquipAmmo.BulletNowCount);
            weaponUI.ChangeBulletUIColor((nowEquipAmmo.RemainBulletPercent >= 0.4f) ? Color.white : Color.red);
            Debug.LogFormat("총알 발사! 남은 총알 개수 : {0}", nowEquipAmmo.BulletNowCount);

            RaycastHit ray;
            if (Physics.Raycast(bulletTransform.position, bulletTransform.forward, out ray, float.MaxValue))
            {
                if (ray.transform.TryGetComponent<IHittable>(out IHittable hit))
                {
                    hit.Hit(weapon.Damage);
                    hitExplosionParticlePool.ActiveParticle(ray.point);
                }
            }

            Recoil();
            Haptic(0.5f);
            ActiveMuzzleParticle();
            StartCoroutine(ShootCoolTime());
        }

        else
        {
            CPlayerSoundManager.Instance.PlaySoundOneShot(weapon.SoundEmptyShot);

            RaycastHit ray;

            if (Physics.Raycast(bulletTransform.position, bulletTransform.forward, out ray, float.MaxValue))
            {
                if (ray.transform.TryGetComponent<IHittable>(out IHittable hit))
                {
                    hit.Hit(weapon.Damage);
                    hitExplosionParticlePool.ActiveParticle(ray.point);
                }
            }

            Haptic(0.1f);
            Debug.LogFormat("남은 총알 개수가 없다!");
        }
    }

    /// <summary>
    /// 총 발사 쿨타임을 기다리는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootCoolTime()
    {
        isFireReady = false;

        yield return new WaitForSeconds(weapon.ShootCoolTime);

        isFireReady = true;
    }

    /// <summary>
    /// 컨트롤러를 진동시키기 위한 메서드
    /// </summary>
    public void Haptic(float duration)
    {
        if (leftController is not null)
        {
            Debug.Log("왼쪽 손 진동 발생!");
            leftController.SendHapticImpulse(0.8f, duration);
        }

        if (rightController is not null)
        {
            Debug.Log("오른쪽 손 진동 발생!");
            rightController.SendHapticImpulse(0.8f, duration);
        }
    }

    /// <summary>
    /// 탄창을 추가한다.
    /// </summary>
    /// <param name="args">SelectEtnerEvent</param>
    public void AddAmmo(SelectEnterEventArgs args)
    {
        nowEquipAmmo = args.interactableObject.transform.GetComponent<CAmmo>();
        nowEquipAmmo.GetComponent<Rigidbody>().isKinematic = false;
    }

    /// <summary>
    /// 탄창을 제거한다.
    /// </summary>
    /// <param name="args">SelectExitEvent</param>
    public void RemoveAmmo(SelectExitEventArgs args)
    {
        nowEquipAmmo = null;
    }

    /// <summary>
    /// 장전 손잡이가 Slide 됐을 때 실행 (재장전)
    /// </summary>
    public void Slide()
    {
        if (nowEquipAmmo is not null)
        {
            CPlayerSoundManager.Instance.PlaySoundOneShot(weapon.SoundReload);

            weapon.Reload(nowEquipAmmo.BulletNowCount);

            weaponUI.ChangeBulletCount(nowEquipAmmo.BulletNowCount);
            weaponUI.ChangeBulletUIColor((nowEquipAmmo.RemainBulletPercent >= 0.4f) ? Color.white : Color.red);
            Debug.LogFormat("장전 완료 : {0}", nowEquipAmmo.BulletNowCount);
        }

        else
        {
            Debug.LogFormat("장전 실패");
        }
    }

    /// <summary>
    /// Trigger시 총기 반동을 주기위한 Coroutine을 실행하는 메서드
    /// </summary>
    public void Recoil()
    {
        Debug.Log("Recoil Work");
        StartCoroutine(RecoilStart());
    }

    /// <summary>
    /// 총에 반동을 준다.
    /// </summary>
    /// <returns></returns>
    IEnumerator RecoilStart()
    {
        float fStartTime = 0.0f;
        while (fStartTime <= weapon.RecoilTime)
        {
            modelTransform.Translate(Vector3.forward * -3.0f * Time.deltaTime);
            modelTransform.Rotate(Vector3.right * -60.0f * Time.deltaTime);

            fStartTime += Time.deltaTime;

            yield return null;
        }

        fStartTime = 0.0f;
        while (fStartTime <= weapon.RecoilTime)
        {
            modelTransform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
            modelTransform.Rotate(Vector3.right * 60.0f * Time.deltaTime);

            fStartTime += Time.deltaTime;

            yield return null;
        }

        modelTransform.SetLocalPositionAndRotation(modelStartPosition, Quaternion.identity);
    }

    /// <summary>
    /// Fire시 재생될 파티클을 키고 끄는 메서드
    /// </summary>
    public void ActiveMuzzleParticle()
    {
        oMuzzleParticle.SetActive(true);
        Invoke("InActiveMuzzleParticle", 0.1f);
    }

    /// <summary>
    /// Active된 Muzzle 파티클을 다시 InActive 시키는 메서드
    /// </summary>
    public void InActiveMuzzleParticle()
    {
        oMuzzleParticle.SetActive(false);
    }
}