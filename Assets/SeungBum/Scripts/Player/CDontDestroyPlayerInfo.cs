using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDontDestroyPlayerInfo : MonoBehaviour
{
    #region public º¯¼ö
    public List<GameObject> dontDestoryObejcts;
    #endregion

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < dontDestoryObejcts.Count; i++)
        {
            DontDestroyOnLoad(dontDestoryObejcts[i]);
        }
    }
}
