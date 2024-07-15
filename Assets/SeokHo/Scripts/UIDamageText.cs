using UnityEngine;

public class UIDamageText : MonoBehaviour
{
    public Vector3 offset = Vector3.zero; // ������ �ؽ�Ʈ ��ġ ������
    public Transform enemyTransform; // ���� Ʈ������

    // ������ �ؽ�Ʈ�� ���� ��ġ�� ���� �����̵��� ����

    void Update()
    {
        if (enemyTransform != null)
        {
            // ���� ��ġ�� �������� ���� ��ġ�� ����
            transform.position = enemyTransform.position + offset;
            // ī�޶� ���ϵ��� ȸ��
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0); // LookAt�� �ؽ�Ʈ�� �ݴ� �������� ���� ������ 180�� ȸ��
        }
    }

    //void OnEnable()
    //{
    //    // �ð��� ���� ������ �ؽ�Ʈ �̵�
    //    transform.position += new Vector3(0f, 1f * Time.deltaTime, 0f);
    //}

    // �� Ʈ�������� �������� �����ϴ� �ʱ�ȭ �޼���
    public void Initialize(Transform enemy, Vector3 offset)
    {
        this.enemyTransform = enemy;
        this.offset = offset;
    }
}
