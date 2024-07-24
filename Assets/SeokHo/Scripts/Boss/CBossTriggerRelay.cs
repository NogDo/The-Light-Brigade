using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossTriggerRelay : MonoBehaviour
{
    public CBossCircleIceShards parentScript;

    private void OnTriggerEnter(Collider other)
    {
        parentScript.OnChildTriggerEnter(other);
    }
}
