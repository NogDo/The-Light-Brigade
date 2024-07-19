using UnityEngine;

public class CBullet : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject bulletParticlePrefab;
    public GameObject muzzleParticlePrefab;
    public float colliderRadius = 1f;
    [Range(0f, 1f)]
    public float collideOffset = 0.15f;

    private Rigidbody rb;
    private Transform myTransform;
    private SphereCollider sphereCollider;
    public BulletPool bulletPool;

    private GameObject bulletParticle;
    private GameObject muzzleParticle;
    private float destroyTimer = 0f;
    private bool destroyed = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myTransform = transform;
        sphereCollider = GetComponent<SphereCollider>();

        bulletPool = FindObjectOfType<BulletPool>();
    }

    void OnEnable()
    {
        destroyed = false;
        destroyTimer = 0f;

        bulletParticle = Instantiate(bulletParticlePrefab, myTransform.position, myTransform.rotation);
        bulletParticle.transform.parent = myTransform;

        if (muzzleParticlePrefab)
        {
            muzzleParticle = Instantiate(muzzleParticlePrefab, myTransform.position, myTransform.rotation);
            muzzleParticle.transform.parent = myTransform;
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

        RaycastHit hit;
        if (Physics.SphereCast(myTransform.position, rad, dir, out hit, dist))
        {
            myTransform.position = hit.point + (hit.normal * collideOffset);

            GameObject impactP = Instantiate(impactParticle, myTransform.position, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(impactP, 5.0f);

            if (hit.transform.CompareTag("Player"))
            {
                Destroy(hit.transform.gameObject);
            }
            DestroyBullet();
        }
        else
        {
            destroyTimer += Time.deltaTime;

            if (destroyTimer >= 5f)
            {
                DestroyBullet();
            }
        }

        RotateTowardsDirection();
    }

    private void DestroyBullet()
    {
        destroyed = true;
        Destroy(bulletParticle, 3f);
        bulletPool.ReturnBullet(gameObject);
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