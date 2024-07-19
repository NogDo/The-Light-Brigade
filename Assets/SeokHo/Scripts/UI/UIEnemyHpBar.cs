using UnityEngine;

public class UIEnemyHpBar : MonoBehaviour
{
    private RectTransform recttrHp; // 자신의 RectTransform 저장할 변수
    public Vector3 v3offset = Vector3.zero; // HpBar 위치 조절용
    public Transform trEnemy; // 적 캐릭터의 위치

    void Start()
    {
        recttrHp = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (trEnemy != null)
        {
            // 적의 월드 포지션에 오프셋을 더한 값을 설정
            Vector3 worldPosition = trEnemy.position + v3offset;

            // RectTransform의 위치를 월드 포지션으로 설정
            recttrHp.position = worldPosition;

            // HP 바가 카메라를 향하도록 설정
            transform.LookAt(Camera.main.transform);
        }
    }
}
