using UnityEngine;

public class UIEnemyHpBar : MonoBehaviour
{
    private RectTransform rectTransformHp; // �ڽ��� RectTransform ������ ����
    public Vector3 offset = Vector3.zero; // HpBar ��ġ ������
    public Transform enemyTransform; // �� ĳ������ ��ġ

    void Start()
    {
        rectTransformHp = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        rectTransformHp.position = enemyTransform.position + offset;
    }
}
