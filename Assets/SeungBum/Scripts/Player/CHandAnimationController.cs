using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CHandAnimationController : MonoBehaviour
{
    #region public 변수
    public Animator animator;
    public Transform tfHandOffsetNode;
    public Transform tfGloveMasterOffset;
    public Transform tfGrabPoseMovemonetNode;
    #endregion

    #region private 변수
    XRRayInteractor rayInteractor;
    XRDirectInteractor directInteractor;

    InputActionProperty triggerAction;
    InputActionProperty selectAction;

    Vector3 v3GloveMasterOffset;
    Quaternion qMeshOffset;
    Quaternion qGloveMasterOffset;
    Quaternion qGrabPoseMovemonetNode;

    [SerializeField]
    bool isAnimatedHand;
    #endregion

    void Start()
    {
        //rayInteractor = transform.parent.GetComponentInChildren<XRRayInteractor>();
        //rayInteractor.selectEntered.AddListener(OnSelectEntered);
        //rayInteractor.selectExited.AddListener(OnSelectExited);

        //directInteractor = transform.parent.GetComponentInChildren<XRDirectInteractor>();
        //directInteractor.selectEntered.AddListener(OnSelectEntered);
        //directInteractor.selectExited.AddListener(OnSelectExited);

        if (!isAnimatedHand)
        {
            triggerAction = transform.parent.GetComponent<ActionBasedController>().activateAction;
            selectAction = transform.parent.GetComponent<ActionBasedController>().selectAction;
            InitAction();

            qMeshOffset = tfHandOffsetNode.localRotation;
            qGloveMasterOffset = tfGloveMasterOffset.localRotation;
            v3GloveMasterOffset = tfGloveMasterOffset.localPosition;
            qGloveMasterOffset = tfGrabPoseMovemonetNode.localRotation;
        }
    }

    /// <summary>
    /// RayInteractor, DirectInterator가 SelectEntered됐을 때 실행될 메서드
    /// </summary>
    /// <param name="args"></param>
    void OnSelectEntered(SelectEnterEventArgs args)
    {
        ResetPose();

        //animator.SetInteger("Action", args.interactableObject.transform.GetComponent<CInteractable>().ActionNumber);
    }

    /// <summary>
    /// RayInteractor, DirectInterator가 SelectExited됐을 때 실행될 메서드
    /// </summary>
    /// <param name="args"></param>
    void OnSelectExited(SelectExitEventArgs args)
    {
        InitPose();

        //animator.SetInteger("Action", 0);
    }

    /// <summary>
    /// Hand Action 애니메이션을 재생
    /// </summary>
    /// <param name="actionNumber">재생할 애니메이션 Number</param>
    public void ActionAnimation(int actionNumber)
    {
        if (!isAnimatedHand)
        {
            if (actionNumber == 0)
            {
                InitPose();
            }

            else
            {
                ResetPose();
            }
        }

        animator.SetInteger("Action", actionNumber);
    }

    /// <summary>
    /// 플레이어 Hand FBX 상위 부모들의 Position, Rotation값을 0으로 초기화 시키기 위한 메서드 
    /// (Grab 됐을 때 Animation에 따른 Hand 위치의 동기화를 위함)
    /// </summary>
    void ResetPose()
    {
        tfHandOffsetNode.localRotation = Quaternion.identity;

        tfGloveMasterOffset.localPosition = Vector3.zero;
        tfGloveMasterOffset.localRotation = Quaternion.identity;

        tfGrabPoseMovemonetNode.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// 플레이어 Hand FBX 상위 부모들의 Position, Rotation 값을 다시 초기값으로 돌려주는 메서드
    /// (Grab이 풀렸을 때 Hand 위치의 동기화를 위함)
    /// </summary>
    void InitPose()
    {
        tfHandOffsetNode.localRotation = qMeshOffset;

        tfGloveMasterOffset.localPosition = v3GloveMasterOffset;
        tfGloveMasterOffset.localRotation = qGloveMasterOffset;

        tfGrabPoseMovemonetNode.localRotation = qGrabPoseMovemonetNode;
    }

    /// <summary>
    /// Trigger, Grab 버튼을 눌렀을 때 Hand 애니메이션을 실행하는 Action을 추가한다.
    /// </summary>
    void InitAction()
    {
        triggerAction.action.performed += (InputAction.CallbackContext context) => { animator.SetFloat("Grab", 1.0f); };
        triggerAction.action.canceled += (InputAction.CallbackContext context) => { animator.SetFloat("Grab", 0.0f); };
        selectAction.action.performed += (InputAction.CallbackContext context) => { animator.SetFloat("Grab", 2.0f); };
        selectAction.action.canceled += (InputAction.CallbackContext context) => { animator.SetFloat("Grab", 0.0f); };
    }
}
