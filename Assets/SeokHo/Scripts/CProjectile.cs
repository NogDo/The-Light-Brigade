using UnityEngine;
using System.Collections;

public class CProjectile : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public float colliderRadius = 1f;
    [Range(0f, 1f)]
    public float collideOffset = 0.15f;

    private Rigidbody rb;
    private Transform myTransform;
    private SphereCollider sphereCollider;

    private float destroyTimer = 0f;
    private bool destroyed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myTransform = transform;
        sphereCollider = GetComponent<SphereCollider>();

        projectileParticle = Instantiate(projectileParticle, myTransform.position, myTransform.rotation) as GameObject;
        projectileParticle.transform.parent = myTransform;

        if (muzzleParticle)
        {
            muzzleParticle = Instantiate(muzzleParticle, myTransform.position, myTransform.rotation) as GameObject;

            Destroy(muzzleParticle, 1.5f);
        }
    }

    void FixedUpdate()
    {
        if (destroyed)
        {
            return;
        }

        float rad = sphereCollider ? sphereCollider.radius : colliderRadius;

        Vector3 dir = rb.velocity;
        float dist = dir.magnitude * Time.deltaTime;

        //if (rb.useGravity)
        //{
        //    dir += Physics.gravity * Time.deltaTime;
        //    dist = dir.magnitude * Time.deltaTime;
        //}

        RaycastHit hit;
        if (Physics.SphereCast(myTransform.position, rad, dir, out hit, dist))
        {
            myTransform.position = hit.point + (hit.normal * collideOffset);

            GameObject impactP = Instantiate(impactParticle, myTransform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;

            if (hit.transform.tag == "Destructible") 
            {
                Destroy(hit.transform.gameObject);
            }
            Destroy(projectileParticle, 3f);
            Destroy(impactP, 5.0f);
            DestroyMissile();
        }
        else
        {
            destroyTimer += Time.deltaTime;

            if (destroyTimer >= 5f)
            {
                DestroyMissile();
            }
        }

        RotateTowardsDirection();
    }

    private void DestroyMissile()
    {
        destroyed = true;

        Destroy(projectileParticle, 3f);
        Destroy(gameObject);

        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
        for (int i = 1; i < trails.Length; i++)
        {
            ParticleSystem trail = trails[i];
            if (trail.gameObject.name.Contains("Trail"))
            {
                trail.transform.SetParent(null);
                Destroy(trail.gameObject, 2f);
            }
        }
    }

    private void RotateTowardsDirection()
    {
        if (rb.velocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized, Vector3.up);
            float angle = Vector3.Angle(myTransform.forward, rb.velocity.normalized);
            float lerpFactor = angle * Time.deltaTime; 
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, lerpFactor);
        }
    }
}
