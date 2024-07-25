using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && SceneLoadManager.Instance != null)
        {
            Debug.Log("OnTriggerEnter");
            
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            
            SceneLoadManager.Instance.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("SceneLoadManager instance not found or the collider did not hit the player.");
        }
    }
}