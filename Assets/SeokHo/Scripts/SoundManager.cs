using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static SoundManager Instance { get; private set; }

    // ����� �ҽ� ������ (AudioSource�� ���Ե� ������)
    public AudioSource audioSourcePrefab;
    // ���� ���� Ŭ�� �迭
    public AudioClip[] enemySounds;

    private void Awake()
    {
        // �ν��Ͻ��� �������� �ʴ� ���, ���� �ν��Ͻ��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ���� �� ������Ʈ�� �ı����� ����
        }
        else
        {
            // �ߺ� �ν��Ͻ��� �ı�
            Destroy(gameObject);
        }
    }

    // ���� ���带 ����ϴ� �޼ҵ�
    public void PlayEnemySound(int soundIndex, Vector3 position)
    {
        // ���� �ε����� �迭 ������ ����� �ʵ��� Ȯ��
        if (soundIndex < 0 || soundIndex >= enemySounds.Length)
        {
            Debug.LogWarning("���� �ε����� ������ ������ϴ�");
            return;
        }

        // ���ο� ����� �ҽ� ������ �ν��Ͻ� ����
        AudioSource audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        audioSource.clip = enemySounds[soundIndex]; // ���� Ŭ�� �Ҵ�
        audioSource.spatialBlend = 1.0f; // 3D ����� ����
        audioSource.Play(); // ���� ���

        // ���� ��� �� ����� �ҽ� ������Ʈ�� �ɼǿ� ���� �ı�
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}
