using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] scenes; // �ε��� ���� �̸� �迭
    private int currentSceneIndex = 0; // ���� �ε��� ���� �ε���

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
            currentSceneIndex++; // ���� ������ �ε��� ����
        }
        else
        {
            Debug.Log("��� ���� �ε��߽��ϴ�.");
        }
    }
}