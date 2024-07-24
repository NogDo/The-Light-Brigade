using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerStats : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    TMP_Text tmpLife;
    [SerializeField]
    TMP_Text tmpHP;
    [SerializeField]
    TMP_Text tmpSoul;
    [SerializeField]
    TMP_Text tmpAmmo;
    #endregion

    /// <summary>
    /// Player Stats UI의 Life Text 변경
    /// </summary>
    /// <param name="nowLifeCount">플레이어 현재 목숨</param>
    /// <param name="maxLifeCount">플레이어 최대 목숨</param>
    public void ChangeLifeCount(int nowLifeCount, int maxLifeCount)
    {
        tmpLife.text = $"{nowLifeCount}/{maxLifeCount}";
    }

    /// <summary>
    /// Player Stats UI의 HP Text 변경
    /// </summary>
    /// <param name="nowHPCount">플레이어 현재 체력</param>
    /// <param name="maxHPCount">플레이어 최대 체력</param>
    public void ChangeHPText(int nowHPCount, int maxHPCount)
    {
        tmpHP.text =
            (nowHPCount >= 4) ?
            $"<color=white>{nowHPCount}</color>/{maxHPCount}"
            :
            $"<color=red>{nowHPCount}</color>/{maxHPCount}";
    }

    /// <summary>
    /// Player Stats UI의 Soul Text 변경
    /// </summary>
    /// <param name="soulCount">플레이어 보유 영혼</param>
    public void ChangeSoulText(int soulCount)
    {
        tmpSoul.text = soulCount.ToString();
    }

    /// <summary>
    /// Player Stats UI의 Ammo Text 변경
    /// </summary>
    /// <param name="ammoCount">플레이어 보유 탄창</param>
    public void ChangeAmmoText(int ammoCount)
    {
        tmpAmmo.text = ammoCount.ToString();
    }
}