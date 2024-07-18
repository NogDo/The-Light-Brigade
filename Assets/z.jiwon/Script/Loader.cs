using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if (SceneLoadManager.Instance != null && other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter");
            SceneLoadManager.Instance.LoadScene();
        }
       else
        {
            Debug.LogError("SceneLoadManager instance is not available.");
        }
    }
}
