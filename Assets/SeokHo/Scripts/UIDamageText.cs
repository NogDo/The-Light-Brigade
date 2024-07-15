using System.Collections;
using TMPro;
using UnityEngine;

public class UIDamageText : MonoBehaviour
{
    public Vector3 v3offset = Vector3.zero; // ������ �ؽ�Ʈ ��ġ ������
    public Transform trEnemy;// ���� Ʈ������
    private UIDamagePool damagePool;

    void Start()
    {
        damagePool = FindObjectOfType<UIDamagePool>();
    }

    void Update()
    {
        if (trEnemy != null)
        {
            // ���� ��ġ�� �������� ���� ��ġ�� ����
            transform.position = trEnemy.position + v3offset;
            // ī�޶� ���ϵ��� ȸ��
            transform.LookAt(Camera.main.transform);
            // LookAt�� �ؽ�Ʈ�� �ݴ� �������� ���� ������ 180�� ȸ��
            transform.Rotate(0, 180, 0);
        }
    }

    public void Initialize(Transform enemy, Vector3 offset, UIDamagePool pool)
    {
        this.trEnemy = enemy;
        this.v3offset = offset;
        this.damagePool = pool;
        transform.position = enemy.position + offset;
        StartCoroutine(FadeAndMove());
    }

    private IEnumerator FadeAndMove()
    {
        TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();
        Color originalColor = textMesh.color;
        float originalFontSize = textMesh.fontSize;
        float duration = 1f;
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, 1f, 0);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // �ð��� ������ �ö󰡰Բ�
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // �ð��� ������ ������Բ�
            textMesh.color = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, 0), t);

            // �ð��� ������ ��Ʈ ũ�Ⱑ �۾����Բ�
            textMesh.fontSize = Mathf.Lerp(originalFontSize, 0, t);

            yield return null;
        }

        // ��Ȱ���� ���� �� �ʱ�ȭ
        transform.position = startPosition;
        textMesh.color = originalColor;
        textMesh.fontSize = originalFontSize;

        // ������Ʈ�� Ǯ�� ��ȯ
        damagePool.ReturnObject(gameObject);
        
    }
}