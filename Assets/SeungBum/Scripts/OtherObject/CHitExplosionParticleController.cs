using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHitExplosionParticleController : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("InActiveParticle", 1.0f);
    }


    void InActiveParticle()
    {
        gameObject.SetActive(false);
    }
}
