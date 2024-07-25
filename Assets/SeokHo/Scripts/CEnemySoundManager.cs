using UnityEngine;

public class CEnemySoundManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static CEnemySoundManager Instance { get; private set; }

    // 오디오 소스 프리팹 (AudioSource가 포함된 프리팹)
    public AudioSource audioSourcePrefab;
    // 적의 사운드 클립 배열
    public AudioClip[] enemySounds;

    public AudioClip[] BossSounds;

   

    private void Awake()
    {
        // audioSourcePrefab = GetComponent<AudioSource>();
        // 인스턴스가 존재하지 않는 경우, 현재 인스턴스를 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경 시 오브젝트를 파괴하지 않음
        }
        else
        {
            // 중복 인스턴스는 파괴
            Destroy(gameObject);
        }
    }

    // 적의 사운드를 재생하는 메소드
    public void PlayEnemySound(int soundIndex, Vector3 position)
    {
        // 사운드 인덱스가 배열 범위를 벗어나지 않도록 확인
        if (soundIndex < 0 || soundIndex >= enemySounds.Length)
        {
            Debug.LogWarning("사운드 인덱스가 범위를 벗어났습니다");
            return;
        }

        // 새로운 오디오 소스 프리팹 인스턴스 생성
        AudioSource audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        audioSource.clip = enemySounds[soundIndex]; // 사운드 클립 할당
        audioSource.spatialBlend = 1.0f; // 3D 사운드로 설정
        audioSource.Play(); // 사운드 재생

        // 사운드 재생 후 오디오 소스 오브젝트를 옵션에 따라 파괴
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    // 적의 사운드를 재생하는 메소드
    public void PlayBossSound(int soundIndex, Vector3 position)
    {
        // 사운드 인덱스가 배열 범위를 벗어나지 않도록 확인
        if (soundIndex < 0 || soundIndex >= BossSounds.Length)
        {
            Debug.LogWarning("사운드 인덱스가 범위를 벗어났습니다");
            return;
        }

        // 새로운 오디오 소스 프리팹 인스턴스 생성
        AudioSource audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        audioSource.clip = BossSounds[soundIndex]; // 사운드 클립 할당
        audioSource.spatialBlend = 1.0f; // 3D 사운드로 설정
        audioSource.Play(); // 사운드 재생

        // 사운드 재생 후 오디오 소스 오브젝트를 옵션에 따라 파괴
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}
