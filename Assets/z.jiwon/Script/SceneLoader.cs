using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] sceneNames;
    private int curruntSceneIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        
        if (other.CompareTag("Player"))
        {
            curruntSceneIndex = (curruntSceneIndex + 1) % sceneNames.Length;
            SceneManager.LoadScene(sceneNames[curruntSceneIndex]);
        }
    }


}
