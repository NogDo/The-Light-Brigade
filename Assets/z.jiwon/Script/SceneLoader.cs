using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] scenes; // �ε��� ���� �̸� �迭
    private int currentSceneIndex = 0; // ���� �ε��� ���� �ε���

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentSceneIndex < scenes.Length)
        {
            Debug.Log("Current Scene Index: " + currentSceneIndex);
            PlayerPrefs.SetInt("NextSceneIndex", currentSceneIndex);
            SceneManager.LoadScene("LoadingScene");
        }
    }
    public void LoadNextScene()
    {
        if (currentSceneIndex < scenes.Length - 1)
        {
            currentSceneIndex++;
            PlayerPrefs.SetInt("NextSceneIndex", currentSceneIndex);
        }
        else
        {
            Debug.Log("End of game, no more scenes to load.");
        }
    }
}