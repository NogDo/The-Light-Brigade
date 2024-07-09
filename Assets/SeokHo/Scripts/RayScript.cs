using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    RaycastHit raycasthit;
    float Maxdistance = 300f;

    MeshRenderer render;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Debug.DrawRay(transform.position, transform.forward * Maxdistance, Color.blue, 0.3f);

            // 맞았을 시
            if(Physics.Raycast(transform.position,transform.forward,out raycasthit, Maxdistance)) 
            {
                //hit.transform.GetComponent<MeshRenderer>().enabled = false;
                //hit.transform.GetComponent<Animator>().SetTrigger("Hit");

                if (raycasthit.transform.gameObject.TryGetComponent<IHittable>(out IHittable target))
                {
                    target.Hit();

                    Invoke("SetActiveTrue", 3.0f);
                }
            }
        }
    }
}
