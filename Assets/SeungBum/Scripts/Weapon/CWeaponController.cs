using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CWeaponController : MonoBehaviour
{
    #region private ����
    XRGrabInteractable grabInteractable;
    Animator animator;
    CWeapon weapon;
    CAmmo nowEquipAmmo;
    #endregion

    #region public ����
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
    /// �Ѿ��� �߻��ϱ����� �޼���
    /// </summary>
    /// <param name="eventArgs">ActiveEvent</param>
    public void Fire(ActivateEventArgs eventArgs)
    {
        if (nowEquipAmmo is not null && nowEquipAmmo.BulletCount > 0)
        {
            RaycastHit hit;

            nowEquipAmmo.DecreaseBulltCount();
            Debug.LogFormat("�Ѿ� �߻�! ���� �Ѿ� ���� : {0}", nowEquipAmmo.BulletCount);

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
            Debug.LogFormat("���� �Ѿ� ������ ����!");
        }
    }

    /// <summary>
    /// źâ�� �߰��Ѵ�.
    /// </summary>
    /// <param name="args">SelectEtnerEvent</param>
    public void AddAmmo(SelectEnterEventArgs args)
    {
        nowEquipAmmo = args.interactableObject.transform.GetComponent<CAmmo>();
        Debug.LogFormat("źâ ���� : {0}", nowEquipAmmo.name);
    }

    /// <summary>
    /// źâ�� �����Ѵ�.
    /// </summary>
    /// <param name="args">SelectExitEvent</param>
    public void RemoveAmmo(SelectExitEventArgs args)
    {
        nowEquipAmmo = null;
        Debug.Log("źâ ����");
    }

    /// <summary>
    /// ���� �����̰� Slide ���� �� ���� (������)
    /// </summary>
    public void Slide()
    {
        if (nowEquipAmmo is not null)
        {
            weapon.Reload(nowEquipAmmo.BulletCount);
            Debug.LogFormat("���� �Ϸ� : {0}", nowEquipAmmo.BulletCount);
        }

        else
        {
            Debug.LogFormat("���� ���� : {0}", nowEquipAmmo.BulletCount);
        }
    }
}