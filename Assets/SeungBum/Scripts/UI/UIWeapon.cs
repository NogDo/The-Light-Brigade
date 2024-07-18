using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWeapon : MonoBehaviour
{
    #region private 변수
    [SerializeField]
    Image imgBullet;
    [SerializeField]
    Image imgHP;
    [SerializeField]
    TMP_Text tmpBulletCount;
    [SerializeField]
    TMP_Text tmpHP;
    #endregion

    /// <summary>
    /// 총알 개수 Text를 변경한다.
    /// </summary>
    /// <param name="count">남은 총알 개수</param>
    public void ChangeBulletCount(int count)
    {
        tmpBulletCount.text = count.ToString();
    }

    /// <summary>
    /// 총알 UI(Image, TMP)의 Color를 변경한다.
    /// </summary>
    /// <param name="color">변경할 색</param>
    public void ChangeBulletUIColor(Color color)
    {
        imgBullet.color = color;
        tmpBulletCount.color = color;
    }

    /// <summary>
    /// HP Text를 변경한다.
    /// </summary>
    /// <param name="count">남은 HP</param>
    public void ChangeHPCount(int count)
    {
        tmpHP.text = count.ToString();
    }

    /// <summary>
    /// HP UI(Image, TMP)의 Color를 변경한다.
    /// </summary>
    /// <param name="color">변경할 색</param>
    public void ChangeHPUIColor(Color color)
    {
        imgHP.color = color;
        tmpHP.color = color;
    }
}