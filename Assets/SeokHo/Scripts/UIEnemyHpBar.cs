using UnityEngine;

public class UIEnemyHpBar : MonoBehaviour
{
    private RectTransform rectTransformHp; // 자신의 RectTransform 저장할 변수
    public Vector3 offset = Vector3.zero; // HpBar 위치 조절용
    public Transform enemyTransform; // 적 캐릭터의 위치

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
