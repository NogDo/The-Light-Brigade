using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] ScenesToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadScenesInOrder());
        }
    }

    IEnumerator LoadScenesInOrder()
    {
        foreach (var sceneName in ScenesToLoad)
        {
            yield return StartCoroutine(LoadSceneAsync(sceneName));
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        Debug.Log($"Loading{sceneName}...");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log($"{sceneName} loaded");
    }
}
