using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponActionNumberChangeControl : MonoBehaviour
{
    #region private ����
    CWeapon weapon;
    #endregion

    void Start()
    {
        weapon = transform.parent.GetComponent<CWeapon>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            
        }
    }
}