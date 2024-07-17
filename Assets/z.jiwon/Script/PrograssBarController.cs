using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrograssBarController : MonoBehaviour
{
    [SerializeField] Image progressBar;

    void Start()
    {
        int sceneIndex = PlayerPrefs.GetInt("NextSceneIndex", 0); // 기본값은 0
        StartCoroutine(LoadSceneProgress(sceneIndex));
    }

    IEnumerator LoadSceneProgress(int sceneIndex)
    {
        Debug.Log("Loading Scene Index: " + sceneIndex);
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
        op.allowSceneActivation = false;


        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                progressBar.fillAmount = Mathf.Clamp01(op.progress / 0.9f);
                yield return new WaitForSeconds(3);
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}