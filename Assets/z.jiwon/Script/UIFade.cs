using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    private CanvasGroup cg;
    public float fadeTime = 1f;
    float accumTime = 0f;
    private Coroutine fadeCor;
    public Collider UiCollider;

    private void Awake()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartFadeIn();
        }
    }

    public void StartFadeIn()
    {
        if (fadeCor != null)
        {
            Debug.Log("스타트 페이드인 시작");
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("페이드 인 시작");
        yield return new WaitForSeconds(0.2f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 1f;
    }
}
