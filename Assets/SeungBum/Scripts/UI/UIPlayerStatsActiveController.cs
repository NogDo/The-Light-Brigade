using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UIPlayerStatsActiveController : MonoBehaviour
{
    #region private ����
    [SerializeField]
    Transform leftController;

    CPlayerController playerController;
    UIPlayerStats playerStats;
    #endregion

    void Start()
    {
        playerController = GetComponent<CPlayerController>();
    }

    void Update()
    {
        ActivePlayerStatsUI();
    }

    /// <summary>
    /// ���������� PlayerStatsUI�� Ȱ��ȭ / ��Ȱ��ȭ ��Ų��.
    /// </summary>
    void ActivePlayerStatsUI()
    {
        if (playerStats is null)
        {
            playerStats = playerController.PlayerStatsUI;
            return;
        }

        if (leftController.eulerAngles.z <= 240.0f && leftController.eulerAngles.z >= 150.0f)
        {
            if (playerStats.gameObject.activeSelf)
            {
                return;
            }

            playerStats.gameObject.SetActive(true);
        }

        else
        {
            if (!playerStats.gameObject.activeSelf)
            {
                return;
            }

            playerStats.gameObject.SetActive(false);
        }
    }
}