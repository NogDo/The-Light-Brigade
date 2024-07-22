using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurruntMoney : MonoBehaviour
{
    public TMP_Text goldText; // TextMeshPro 텍스트 컴포넌트
    public CPlayerStats playerStats; // 플레이어 스탯 스크립트

    void Update()
    {
        goldText.text = "Gold: " + playerStats.Money.ToString(); // 골드량 표시
    }
}
