using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager instance;

    public static SceneLoadManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject player;
    public Dictionary<int, Transform> spawnPoints = new Dictionary<int, Transform>();

    private int sceneIndex = 1;

    public void SetSpawnPoint(int sceneIndex, Transform spawnTransform)
    {
        spawnPoints[sceneIndex] = spawnTransform;
    }

    public void LoadScene()
    {
        StartCoroutine(LoadScenes(sceneIndex));
    }

    private IEnumerator LoadScenes(int index)
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(index);
        yield return new WaitUntil(() => sceneLoad.isDone);

        UpdatePlayerPosition(index);
    }

    private void UpdatePlayerPosition(int sceneIndex)
    {
        if (player != null && spawnPoints.ContainsKey(sceneIndex))
        {
            player.transform.position = spawnPoints[sceneIndex].position;
        }
        else
        {
            Debug.LogError("Player or SpawnPoint for scene " + sceneIndex + " is not set correctly.");
        }
    }
}