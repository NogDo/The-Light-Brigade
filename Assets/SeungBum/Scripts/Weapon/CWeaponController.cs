using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CWeaponController : MonoBehaviour
{
    #region private ����
    XRGrabInteractable grabInteractable;
    CWeapon weapon;
    CAmmo nowEquipAmmo;

    ActionBasedController leftController;
    ActionBasedController rightController;

    [SerializeField]
    UIWeapon weaponUI;

    bool isFireReady;
    #endregion

    #region public ����
    public Transform bulletTransform;
    public XRSocketInteractor ammoSoketInteractor;
    #endregion

    void Start()
    {
        //grabInteractable = GetComponent<XRGrabInteractable>();
        //weapon = GetComponent<CWeapon>();
        //nowEquipAmmo = null;

        //grabInteractable.activated.AddListener(Fire);

        //ammoSoketInteractor.selectEntered.AddListener(AddAmmo);
        //ammoSoketInteractor.selectExited.AddListener(RemoveAmmo);

        //isFireReady = true;
    }

    void OnEnable()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        weapon = GetComponent<CWeapon>();
        nowEquipAmmo = null;

        grabInteractable.activated.AddListener(Fire);

        ammoSoketInteractor.selectEntered.AddListener(AddAmmo);
        ammoSoketInteractor.selectExited.AddListener(RemoveAmmo);

        isFireReady = true;
    }

    void OnDisable()
    {
        if (grabInteractable is not null)
        {
            grabInteractable.activated.RemoveListener(Fire);
        }
    }

    /// <summary>
    /// WeaponUI Class ��ȯ
    /// </summary>
    public UIWeapon WeaponUI
    {
        get
        {
            return weaponUI;
        }
    }

    /// <summary>
    /// ���⸦ ���� ������ �׷����� �� leftController�� �Ҵ�
    /// </summary>
    /// <param name="args"></param>
    public void GrabLeftController(SelectEnterEventArgs args)
    {
        leftController = args.interactorObject.transform.parent.GetComponent<ActionBasedController>();
    }

    /// <summary>
    /// ���⸦ ������ ������ �׷����� �� rightController�� �Ҵ�
    /// </summary>
    /// <param name="args"></param>
    public void GrabRightController(SelectEnterEventArgs args)
    {
        rightController = args.interactorObject.transform.parent.GetComponent<ActionBasedController>();
    }

    /// <summary>
    /// ���� ���� �׷��� Release���� �� leftController�� ����
    /// </summary>
    public void ReleaseLeftController()
    {
        leftController = null;
    }

    /// <summary>
    /// ������ ���� �׷��� Release���� �� rightController�� ����
    /// </summary>
    public void ReleaseRightController()
    {
        rightController = null;
    }

    /// <summary>
    /// �Ѿ��� �߻��ϱ����� �޼���
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
            RaycastHit hit;

            nowEquipAmmo.DecreaseBulltCount();

            weaponUI.ChangeBulletCount(nowEquipAmmo.BulletNowCount);
            weaponUI.ChangeBulletUIColor((nowEquipAmmo.RemainBulletPercent >= 0.4f) ? Color.white : Color.red);
            Debug.LogFormat("�Ѿ� �߻�! ���� �Ѿ� ���� : {0}", nowEquipAmmo.BulletNowCount);

            if (Physics.Raycast(bulletTransform.position, bulletTransform.forward, out hit, float.MaxValue))
            {
                if (hit.transform.TryGetComponent<IHittable>(out IHittable hitObj))
                {
                    hitObj.Hit(weapon.Damage);
                }
            }

            Recoil();
            Haptic();
            StartCoroutine(ShootCoolTime());
        }

        else
        {
            Recoil();
            Debug.LogFormat("���� �Ѿ� ������ ����!");
        }
    }

    /// <summary>
    /// �� �߻� ��Ÿ���� ��ٸ��� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootCoolTime()
    {
        isFireReady = false;

        yield return new WaitForSeconds(weapon.ShootCoolTime);

        isFireReady = true;
    }

    /// <summary>
    /// ��Ʈ�ѷ��� ������Ű�� ���� �޼���
    /// </summary>
    public void Haptic()
    {
        if (leftController is not null)
        {
            Debug.Log("���� �� ���� �߻�!");
            leftController.SendHapticImpulse(0.8f, 0.5f);
        }

        if (rightController is not null)
        {
            Debug.Log("������ �� ���� �߻�!");
            rightController.SendHapticImpulse(0.8f, 0.5f);
        }
    }

    /// <summary>
    /// źâ�� �߰��Ѵ�.
    /// </summary>
    /// <param name="args">SelectEtnerEvent</param>
    public void AddAmmo(SelectEnterEventArgs args)
    {
        nowEquipAmmo = args.interactableObject.transform.GetComponent<CAmmo>();
        nowEquipAmmo.GetComponent<Rigidbody>().isKinematic = false;
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
            weapon.Reload(nowEquipAmmo.BulletNowCount);

            weaponUI.ChangeBulletCount(nowEquipAmmo.BulletNowCount);
            weaponUI.ChangeBulletUIColor((nowEquipAmmo.RemainBulletPercent >= 0.4f) ? Color.white : Color.red);
            Debug.LogFormat("���� �Ϸ� : {0}", nowEquipAmmo.BulletNowCount);
        }

        else
        {
            Debug.LogFormat("���� ����");
        }
    }

    /// <summary>
    /// Trigger�� �ѱ� �ݵ��� �ֱ����� Coroutine�� �����ϴ� �޼���
    /// </summary>
    public void Recoil()
    {
        Debug.Log("Recoil Work");
        StartCoroutine(RecoilStart());
    }

    /// <summary>
    /// �ѿ� �ݵ��� �ش�.
    /// </summary>
    /// <returns></returns>
    IEnumerator RecoilStart()
    {
        float fStartTime = 0.0f;

        while (fStartTime <= 0.05f)
        {
            transform.Translate(Vector3.forward * -3.0f * Time.deltaTime);
            transform.Rotate(Vector3.right * -60.0f * Time.deltaTime);

            fStartTime += Time.deltaTime;

            yield return null;
        }
    }
}