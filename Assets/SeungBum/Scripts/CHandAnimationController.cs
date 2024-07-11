using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CHandAnimationController : MonoBehaviour
{
    #region public 변수
    public Animator animator;
    public Transform tfHand;
    #endregion

    #region private 변수
    InputActionProperty grab;
    #endregion

    void Start()
    {
        grab = transform.parent.GetComponent<ActionBasedController>().selectAction;

        grab.action.performed += GrabPerformed;
        grab.action.canceled += GrabCanceld;
    }

    void OnTriggerEnter(Collider other)
    {

    }

    void GrabPerformed(InputAction.CallbackContext context)
    {
        //animator.SetFloat("Grab", 2.5f);
        animator.SetInteger("Action", 1);
    }

    void GrabCanceld(InputAction.CallbackContext context)
    {
        //animator.SetFloat("Grab", 0.0f);
        animator.SetInteger("Action", 0);
    }
}
