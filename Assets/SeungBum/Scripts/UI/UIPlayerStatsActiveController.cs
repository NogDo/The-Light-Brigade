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

    bool isGrab;
    #endregion

    void Start()
    {
        playerController = GetComponent<CPlayerController>();
        isGrab = false;
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
        if (isGrab)
        {
            return;
        }

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

    /// <summary>
    /// Grab상태일 때 PlayerStatsUI를 활성화시키지 않게 하기 위한 메서드
    /// </summary>
    public void DontActive()
    {
        isGrab = true;
    }

    /// <summary>
    /// Left Hand가 Grab상태가 아닐 때 다시 PlayerStatsUI를 활성화시키기 위한 메서드
    /// </summary>
    public void CanActive()
    {
        isGrab = false;
    }
}