using System.Collections;
using UnityEngine;

public class CBGMZone : MonoBehaviour
{
    public AudioClip bgmClip; // 재생할 BGM 클립
    private AudioSource audioSource;
    public float fadeDuration = 1.0f; // 페이드 인/아웃 지속 시간

    private void Start()
    {
        // AudioSource 컴포넌트를 추가하고 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true; // BGM 반복 재생 설정
        audioSource.volume = 0f; // 초기 볼륨을 0으로 설정

        // 플레이어가 영역 안에 있는지 확인
        Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // 플레이어가 영역 안에 있으면 BGM 페이드 인
                StartCoroutine(FadeIn(audioSource, fadeDuration));
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 영역에 들어오면 BGM 페이드 인
            if (!audioSource.isPlaying)
            {
                StartCoroutine(FadeIn(audioSource, fadeDuration));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 영역을 나가면 BGM 페이드 아웃
            StartCoroutine(FadeOut(audioSource, fadeDuration));
        }
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float startVolume = 0f;
        audioSource.volume = startVolume;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = 1.0f; // 볼륨을 최대로 설정
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // 다음 페이드 인을 위해 초기 볼륨 설정
    }
}
