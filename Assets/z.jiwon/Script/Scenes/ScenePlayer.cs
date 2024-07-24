using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePlayer : MonoBehaviour
{
    ScenePlayer instance;

    void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this);
        }
    }
}
