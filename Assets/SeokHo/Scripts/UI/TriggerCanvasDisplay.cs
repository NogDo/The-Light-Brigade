using System.Collections;
using UnityEngine;

public class TriggerCanvasDisplay : MonoBehaviour
{
    public Canvas canvas; // 표시할 Canvas 참조
    public float displayDuration = 5.0f; // Canvas를 표시할 지속 시간
    public float fadeDuration = 1.0f; // 페이드 아웃 효과의 지속 시간

    private void Start()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false); // 초기에는 Canvas를 비활성화
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canvas != null)
        {
            StartCoroutine(DisplayAndFadeCanvas()); // Player가 트리거에 들어오면 코루틴 시작
        }
    }

    private IEnumerator DisplayAndFadeCanvas()
    {
        canvas.gameObject.SetActive(true); // Canvas 활성화
        yield return new WaitForSeconds(displayDuration); // 지정된 시간 동안 대기

        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = canvas.gameObject.AddComponent<CanvasGroup>(); // CanvasGroup이 없으면 추가
        }

        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration); // Canvas 페이드 아웃
            yield return null;
        }

        canvas.gameObject.SetActive(false); // 페이드 아웃 후 Canvas 비활성화
    }
}
