using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    private RectTransform rectHp; // �ڽ��� rectTransform ������ ����

    // HideInInspector�� �ش� ���� �����, ���� ������ �ʿ䰡 ���� �� 
    public Vector3 offset = Vector3.zero; // HpBar ��ġ ������, offset�� ��� HpBar�� ��ġ �������
    public Transform enemyTr; // �� ĳ������ ��ġ

    void Start()
    {
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        rectHp.transform.position = enemyTr.position + offset;
    }
}
