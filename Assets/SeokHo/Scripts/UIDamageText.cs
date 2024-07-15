using UnityEngine;

public class UIDamageText : MonoBehaviour
{
    public Vector3 offset = Vector3.zero; // 데미지 텍스트 위치 오프셋
    public Transform enemyTransform; // 적의 트랜스폼

    // 데미지 텍스트가 적의 위치에 따라 움직이도록 설정

    void Update()
    {
        if (enemyTransform != null)
        {
            // 적의 위치에 오프셋을 더한 위치로 설정
            transform.position = enemyTransform.position + offset;
            // 카메라를 향하도록 회전
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0); // LookAt이 텍스트를 반대 방향으로 보기 때문에 180도 회전
        }
    }

    //void OnEnable()
    //{
    //    // 시간에 따른 데미지 텍스트 이동
    //    transform.position += new Vector3(0f, 1f * Time.deltaTime, 0f);
    //}

    // 적 트랜스폼과 오프셋을 설정하는 초기화 메서드
    public void Initialize(Transform enemy, Vector3 offset)
    {
        this.enemyTransform = enemy;
        this.offset = offset;
    }
}
