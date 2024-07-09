using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    #region private 변수
    MeshRenderer render;
    Animator animator;
    MeshCollider meshCollider;
    Rigidbody body;
    Vector3 v3StartPosition;
    #endregion

    void Awake()
    {
        render = GetComponent<MeshRenderer>();
        animator = transform.parent.parent.GetComponent<Animator>();
    }

    public void Hit()
    {

        animator.SetTrigger("Hit");
        body.isKinematic = false;
        body.useGravity = false;
        meshCollider.isTrigger = false;

        Invoke("SetActiveFalse", 1.0f);
        Invoke("SetActiveTrue", 5.0f);

    }

    void SetActiveFalse()
    {
        render.enabled = false;
    }

    void SetActiveTrue()
    {
        transform.localPosition = v3StartPosition;
        transform.localRotation = Quaternion.identity;
        render.enabled = true;
        body.isKinematic = true;
        body.useGravity = true;
        meshCollider.isTrigger = true;
    }

    #endregion


}