using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurruntMoney : MonoBehaviour
{
    public TMP_Text goldText; // TextMeshPro �ؽ�Ʈ ������Ʈ
    public CPlayerStats playerStats; // �÷��̾� ���� ��ũ��Ʈ

    void Update()
    {
        goldText.text = "���� ���� �ҿ� : " + playerStats.Soul.ToString(); // ��差 ǥ��
    }
}
