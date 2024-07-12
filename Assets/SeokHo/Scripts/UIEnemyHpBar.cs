using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyHpBar : MonoBehaviour
{
    private RectTransform recttrHp; // �ڽ��� rectTransform ������ ����
    public Vector3 v3Offset = Vector3.zero; // HpBar ��ġ ������, offset�� ��� HpBar�� ��ġ �������
    public Transform trEnemy; // �� ĳ������ ��ġ

    void Start()
    {
        recttrHp = this.gameObject.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        recttrHp.transform.position = trEnemy.position + v3Offset;
    }
}
