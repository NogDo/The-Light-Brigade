using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrograssBarController : MonoBehaviour
{
    [SerializeField] Image progressBar;

    void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Map 1");
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
                progressBar.fillAmount = Mathf.Clamp01(op.progress / 0.9f); // 90%를 기준으로 가득 차도록 설정

                // 로딩 완료 후 3초 대기
                yield return new WaitForSeconds(3);

                // 3초 후 씬 활성화
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}