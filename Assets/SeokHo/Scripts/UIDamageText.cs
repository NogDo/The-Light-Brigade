using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamageText : MonoBehaviour
{
    private RectTransform recttrDamageText; // 자신의 rectTransform 저장할 변수
    public Vector3 v3Offset = Vector3.zero; // damageText 위치 조절용, offset은 어디에 damageText를 위치 출력할지
    public Transform trEnemy; // 적 캐릭터의 위치

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
