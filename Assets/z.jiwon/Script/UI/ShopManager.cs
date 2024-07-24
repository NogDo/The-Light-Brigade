using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Button item1Button;
    public Button item2Button;
    public Button item3Button;

    public int item1Cost;
    public int item2Cost;
    public int item3Cost;

    public TMP_Text item1PriceText; // 아이템 1 가격 표시용 TextMeshPro 텍스트
    public TMP_Text item2PriceText; // 아이템 2 가격 표시용 TextMeshPro 텍스트
    public TMP_Text item3PriceText; // 아이템 3 가격 표시용 TextMeshPro 텍스트
    public TMP_Text playerSoulText; // 플레이어의 Soul 값을 표시할 TextMeshPro 텍스트

    private CPlayerStats playerStats;

    void Start()
    {
        playerStats = FindObjectOfType<CPlayerStats>(); // 플레이어 스탯 컴포넌트 찾기
        UpdateButtonStates();
        UpdatePriceTexts();
        UpdateSoulText();
    }

    void UpdateButtonStates()
    {
        // 각 아이템 비용에 따라 버튼 활성화 여부 결정
        item1Button.interactable = playerStats.Soul >= item1Cost;
        item2Button.interactable = playerStats.Soul >= item2Cost;
        item3Button.interactable = playerStats.Soul >= item3Cost;
    }

    void UpdatePriceTexts()
    {
        item1PriceText.text = "Cost: " + item1Cost.ToString(); // 아이템 1 가격 업데이트
        item2PriceText.text = "Cost: " + item2Cost.ToString(); // 아이템 2 가격 업데이트
        item3PriceText.text = "Cost: " + item3Cost.ToString(); // 아이템 3 가격 업데이트
    }

    void UpdateSoulText()
    {
        playerSoulText.text = "Current Soul: " + playerStats.Soul.ToString(); // 현재 Soul 값 업데이트
    }
}