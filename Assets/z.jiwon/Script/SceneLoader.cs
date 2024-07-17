using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] scenes; // 로드할 씬의 이름 배열
    private int currentSceneIndex = 0; // 현재 로드할 씬의 인덱스

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        if (currentSceneIndex < scenes.Length)
        {
            SceneManager.LoadScene(scenes[currentSceneIndex]);
            currentSceneIndex++; // 다음 씬으로 인덱스 증가
        }
        else
        {
            Debug.Log("모든 씬을 로드했습니다.");
        }
    }
}