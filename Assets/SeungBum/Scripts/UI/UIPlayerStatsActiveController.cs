using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UIPlayerStatsActiveController : MonoBehaviour
{
    #region private 변수
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
    /// 각도에따라 PlayerStatsUI를 활성화 / 비활성화 시킨다.
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