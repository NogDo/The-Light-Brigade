using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWeapon : MonoBehaviour
{
    #region private º¯¼ö
    [SerializeField]
    Image imgBullet;
    [SerializeField]
    Image imgHP;
    [SerializeField]
    TMP_Text tmpBulletCount;
    [SerializeField]
    TMP_Text tmpHP;
    #endregion


    public void ChangeBulletCount(int count)
    {
        tmpBulletCount.text = count.ToString();
    }


    public void ChangeColor(Color color)
    {
        imgBullet.color = color;
        tmpBulletCount.color = color;
    }
}