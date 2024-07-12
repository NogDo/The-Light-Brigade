using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyHpBar : MonoBehaviour
{
    private RectTransform recttrHp; // 자신의 rectTransform 저장할 변수
    public Vector3 v3Offset = Vector3.zero; // HpBar 위치 조절용, offset은 어디에 HpBar를 위치 출력할지
    public Transform trEnemy; // 적 캐릭터의 위치

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
