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
            UpdatePlayerPosition(scene.buildIndex); // 위치 업데이트 호출을 여기로 이동
        }
        else
        {
            Debug.LogError("Spawn point not found for scene " + scene.buildIndex);
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

        // 로딩 씬이 제대로 로드되었는지 확인
        if (!SceneManager.GetSceneByName("LoadingScene").isLoaded)
        {
            Debug.LogError("Loading scene failed to load");
            yield break;  // 로딩 씬 로드 실패 시 코루틴 종료
        }

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(index);
        yield return new WaitUntil(() => sceneLoad.isDone);

        // 로딩 씬 언로드 - 씬이 실제로 로드된 것을 확인한 후 언로드 시도
        Scene loadedScene = SceneManager.GetSceneByName("LoadingScene");
        if (loadedScene.isLoaded)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(loadedScene);
            yield return new WaitUntil(() => unloadOperation.isDone);
        }
        else
        {

        }
    }

    private void UpdatePlayerPosition(int sceneIndex)
    {
        if (player != null && spawnPoints.ContainsKey(sceneIndex))
        {
            player.transform.position = spawnPoints[sceneIndex].position;

            for (int i = 0; i < player.transform.childCount; i++)
            {
                player.transform.GetChild(i).transform.localPosition = Vector3.zero;
            }

        }

        else
        {
            Debug.LogError("Player or SpawnPoint for scene " + sceneIndex + " is not set correctly.");
        }
    }
}