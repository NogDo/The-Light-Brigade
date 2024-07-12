using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamageText : MonoBehaviour
{
    private RectTransform recttrDamageText; // �ڽ��� rectTransform ������ ����
    public Vector3 v3Offset = Vector3.zero; // damageText ��ġ ������, offset�� ��� damageText�� ��ġ �������
    public Transform trEnemy; // �� ĳ������ ��ġ

    void Start()
    {
        recttrDamageText = this.gameObject.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        recttrDamageText.transform.position = trEnemy.position + v3Offset;
    }
}
