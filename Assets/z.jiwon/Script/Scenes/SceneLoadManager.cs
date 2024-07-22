using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager instance;

    public static SceneLoadManager Instance
    {
        get { return instance == null ? null : instance; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    int loadingsceneindex = 0;
    int sceneIndex = 1;
    public GameObject player;

    public void LoadScene()
    {
        CPlayerStats playerStats = player.GetComponent<CPlayerStats>();
        if (playerStats.HP <= 0)
        {
            StartCoroutine(LoadInitialScenes());
        }
        else
        {
            StartCoroutine(LoadScenes());
        }
    }
    
    public IEnumerator LoadScenes()
    {
        Debug.Log("�ε� ��");
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(loadingsceneindex);
        yield return new WaitUntil(() => loadingScene.isDone);

        sceneIndex++;
        Debug.LogFormat("���� �� : {0}", sceneIndex);
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneIndex);
        yield return new WaitUntil(() => sceneLoad.isDone);
    }

    IEnumerator LoadInitialScenes()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(0); // 0�� �� �ε�
        yield return new WaitUntil(() => loadingOperation.isDone); // 0�� ���� ������ �ε�� ������ ���

        AsyncOperation gameSceneOperation = SceneManager.LoadSceneAsync(1); // 1�� �� �ε�
        yield return new WaitUntil(() => gameSceneOperation.isDone); // 1�� ���� ������ �ε�� ������ ���
    }

}
