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

    public GameObject player;
    public Dictionary<int, Transform> spawnPoints = new Dictionary<int, Transform>();
    public Image loadingCircle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 0과 씬 1은 스폰 포인트가 필요 없으므로 처리하지 않습니다.
        if (scene.buildIndex == 0 || scene.buildIndex == 1)
        {
            return;
        }

        string spawnPointName = "";
        switch (scene.buildIndex)
        {
            case 2:
                spawnPointName = "spawnPointTp";
                break;
            case 3:
                spawnPointName = "spawnPointM1";
                break;
            case 4:
                spawnPointName = "spawnPointM2";
                break;
            case 5:
                spawnPointName = "spawnPointSt";
                break;
            case 6:
                spawnPointName = "spawnPointBs";
                break;
        }

        GameObject spawnPointGO = GameObject.Find(spawnPointName);
        if (spawnPointGO != null)
        {
            spawnPoints[scene.buildIndex] = spawnPointGO.transform;
            UpdatePlayerPosition(scene.buildIndex);
        }
        else
        {
            
        }
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadScenes(index));
    }

    private IEnumerator LoadScenes(int index)
    {
        AsyncOperation loadLoadingScene = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
        yield return new WaitUntil(() => loadLoadingScene.isDone);

        if (!SceneManager.GetSceneByName("LoadingScene").isLoaded)
        {
            Debug.LogError("Loading scene failed to load");
            yield break;
        }

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(index);
        while (!sceneLoad.isDone)
        {
            float progress = Mathf.Clamp01(sceneLoad.progress / 0.9f);  // Adjust progress to scale from 0 to 1
            loadingCircle.fillAmount = progress;  // 원형 프로그래스 바 업데이트
            yield return null;
        }
        yield return new WaitUntil(() => sceneLoad.isDone);

        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("LoadingScene");
        yield return new WaitUntil(() => unloadOperation.isDone);
    }

    private void UpdatePlayerPosition(int sceneIndex)
    {
        if (player != null && spawnPoints.ContainsKey(sceneIndex))
        {
            player.transform.position = spawnPoints[sceneIndex].position;
        }
        else
        {
            
        }
    }
}