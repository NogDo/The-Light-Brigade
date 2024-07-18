using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager instance;

    public static SceneLoadManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
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

    public void LoadScene()
    {
        StartCoroutine(LoadScenes());
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

}
