using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� �ۼ� ��ũ��Ʈ
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
