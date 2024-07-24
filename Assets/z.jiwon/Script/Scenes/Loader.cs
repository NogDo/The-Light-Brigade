using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && SceneLoadManager.Instance != null)
        {
            Debug.Log("OnTriggerEnter");
            // 현재 씬 인덱스를 기반으로 다음 씬 인덱스를 계산합니다.
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            // SceneLoadManager를 사용하여 다음 씬을 로드합니다.
            SceneLoadManager.Instance.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("SceneLoadManager instance not found or the collider did not hit the player.");
        }
    }
}