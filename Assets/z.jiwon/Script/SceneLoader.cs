using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string loadingSceneName;
    public string[] scenesToLoad;
    public GameObject playerPrefab; // 플레이어 프리팹 참조
    public Transform[] playerSpawnPoints; // 플레이어 스폰 위치 참조 배열

    private Scene? previousScene = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(loadingSceneName, LoadSceneMode.Single);
            StartCoroutine(LoadScenesInOrder());
        }
    }

    IEnumerator LoadScenesInOrder()
    {
        yield return new WaitForSeconds(1); // 로딩 씬이 완전히 로드될 시간을 기다립니다.

        foreach (var sceneName in scenesToLoad)
        {
            if (previousScene.HasValue && previousScene.Value.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(previousScene.Value);
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            Scene currentScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(currentScene);

            if (playerSpawnPoints != null && playerSpawnPoints.Length > 0)
            {
                int index = System.Array.IndexOf(scenesToLoad, sceneName);
                Instantiate(playerPrefab, playerSpawnPoints[index].position, Quaternion.identity);
            }

            previousScene = currentScene;
        }

        if (previousScene.HasValue && previousScene.Value.isLoaded)
        {
            SceneManager.UnloadSceneAsync(previousScene.Value);
        }

        SceneManager.LoadScene(loadingSceneName, LoadSceneMode.Single);
    }
}