using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    private RectTransform rectHp; // 자신의 rectTransform 저장할 변수

    // HideInInspector는 해당 변수 숨기기, 굳이 보여줄 필요가 없을 때 
    public Vector3 offset = Vector3.zero; // HpBar 위치 조절용, offset은 어디에 HpBar를 위치 출력할지
    public Transform enemyTr; // 적 캐릭터의 위치

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
