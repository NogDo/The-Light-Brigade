using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 지원 작성 스크립트
public class BrokenBox : MonoBehaviour
{
    public GameObject fragmentedVersion;
    public GameObject originulVersion;
    bool isGrab = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && isGrab)
        {

            originulVersion.SetActive(false);
            fragmentedVersion.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrab = true;
        }
    }
}
