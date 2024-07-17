using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CWeaponController : MonoBehaviour
{
    #region private 변수
    XRGrabInteractable grabInteractable;
    Animator animator;
    CWeapon weapon;
    CAmmo nowEquipAmmo;
    #endregion

    #region public 변수
    public Transform bulletTransform;
    public XRSocketInteractor ammoSoketInteractor;
    #endregion

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        animator = GetComponent<Animator>();
        weapon = GetComponent<CWeapon>();

        grabInteractable.activated.AddListener(Fire);

        ammoSoketInteractor.selectEntered.AddListener(AddAmmo);
        ammoSoketInteractor.selectExited.AddListener(RemoveAmmo);
    }

    void OnDisable()
    {
        if (grabInteractable is not null)
        {
            grabInteractable.activated.RemoveListener(Fire);
        }
    }

    /// <summary>
    /// 총알을 발사하기위한 메서드
    /// </summary>
    /// <param name="eventArgs">ActiveEvent</param>
    public void Fire(ActivateEventArgs eventArgs)
    {
        if (nowEquipAmmo is not null && nowEquipAmmo.BulletCount > 0)
        {
            RaycastHit hit;

            nowEquipAmmo.DecreaseBulltCount();
            Debug.LogFormat("총알 발사! 남은 총알 개수 : {0}", nowEquipAmmo.BulletCount);

            if (Physics.Raycast(bulletTransform.position, bulletTransform.forward, out hit, float.MaxValue))
            {
                if (hit.transform.TryGetComponent<IHittable>(out IHittable hitObj))
                {
                    hitObj.Hit(weapon.Damage);
                }
            }

            animator.SetTrigger("Fire");
        }

        else
        {
            Debug.LogFormat("남은 총알 개수가 없다!");
        }
    }

    /// <summary>
    /// 탄창을 추가한다.
    /// </summary>
    /// <param name="args">SelectEtnerEvent</param>
    public void AddAmmo(SelectEnterEventArgs args)
    {
        nowEquipAmmo = args.interactableObject.transform.GetComponent<CAmmo>();
        Debug.LogFormat("탄창 장착 : {0}", nowEquipAmmo.name);
    }

    /// <summary>
    /// 탄창을 제거한다.
    /// </summary>
    /// <param name="args">SelectExitEvent</param>
    public void RemoveAmmo(SelectExitEventArgs args)
    {
        nowEquipAmmo = null;
        Debug.Log("탄창 해제");
    }

    /// <summary>
    /// 장전 손잡이가 Slide 됐을 때 실행 (재장전)
    /// </summary>
    public void Slide()
    {
        if (nowEquipAmmo is not null)
        {
            weapon.Reload(nowEquipAmmo.BulletCount);
            Debug.LogFormat("장전 완료 : {0}", nowEquipAmmo.BulletCount);
        }

        else
        {
            Debug.LogFormat("장전 실패 : {0}", nowEquipAmmo.BulletCount);
        }
    }
}