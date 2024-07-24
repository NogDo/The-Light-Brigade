using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CShootingTarget : MonoBehaviour, IHittable
{
    #region 투사체

    //Vector3 v3StartPosition;
    //MeshRenderer render;
    //public Animator animator;
    //MeshCollider meshCollider;
    //Rigidbody body;

    //void Awake()
    //{
    //    body = GetComponent<Rigidbody>();
    //    render = GetComponent<MeshRenderer>();
    //    meshCollider = GetComponent<MeshCollider>();
    //    transform = GetComponent<Transform>();

    //    v3StartPosition = transform.localPosition;
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Bullet"))
    //    {
    //        animator.SetTrigger("Hit");
    //        body.isKinematic = false;
    //        body.useGravity = false;
    //        meshCollider.isTrigger = false;

    //        Invoke("SetActiveFalse", 1.0f);
    //        Invoke("SetActiveTrue", 5.0f);
    //    }

    //}
    //void SetActiveFalse()
    //{
    //    render.enabled = false;
    //}

    //void SetActiveTrue()
    //{
    //    transform.localPosition = v3StartPosition;
    //    transform.localRotation = Quaternion.identity;
    //    render.enabled = true;
    //    body.isKinematic = true;
    //    body.useGravity = true;
    //    meshCollider.isTrigger = true;
    //}
    #endregion

    #region 히트스캔

    #region 변수
    private MeshRenderer render;
    private Animator animator;
    private MeshCollider meshCollider;
    private Rigidbody rb;
    private Vector3 v3StartPosition;
    private Quaternion v3StartRotation;

    public ParticleSystem particleRespawn;
    public ParticleSystem particleBreak;
    bool targetbreak = false;
    #endregion

    void Awake()
    {
        animator = transform.parent.parent.GetComponent<Animator>();
        meshCollider = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
        v3StartPosition = transform.localPosition;
        v3StartRotation = transform.localRotation;
    }

    public void Hit(float damage)
    {
        // 만약 "Target_0500"을 맞췄다면
        if (gameObject.name == "Target_0500")
        {
            Transform parent = transform.parent;

            // 해당 오브젝트의 부모를 기준으로 자식에 CShootingTarget 스크립트가 붙어있다면 전부 처리
            foreach (Transform child in parent)
            {
                CShootingTarget target = child.GetComponent<CShootingTarget>();
                if (target != null)
                {
                    targetbreak = true;
                    // 현재 Invoke 된 모든 것을 취소
                    target.CancelInvoke("SetActiveTrue");

                    // Hit 액션 실행
                    target.TriggerHitActions();
                    if (particleBreak != null)
                    {
                        particleBreak.Play(); // 파티클 재생
                    }
                }
            }
        }
        else
        {
            // 현재 오브젝트의 Hit 액션 실행
            TriggerHitActions();
        }
    }

    private void TriggerHitActions()
    {
        animator.SetTrigger("Hit");
        rb.isKinematic = false;
        meshCollider.isTrigger = false;
        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 0.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized;
        float forceMagnitude = 300f;
        rb.AddForce(randomDirection * forceMagnitude);

        Invoke("SetActiveTrue", 3.0f);
    }

    void SetActiveTrue()
    {
        transform.localPosition = v3StartPosition;
        transform.localRotation = v3StartRotation;

        rb.isKinematic = true;
        meshCollider.isTrigger = true;

        // 만약 Target_0500을 맞췄다면
        if (targetbreak)
        {
            particleRespawn.Play();
        }
    }
    #endregion
}