using System.Collections;
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
    private bool destroyed = false;
    private float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myTransform = transform;
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnEnable()
    {
        destroyed = false;

        if (bulletParticlePrefab)
        {
            bulletParticle = Instantiate(bulletParticlePrefab, myTransform.position, myTransform.rotation);
            bulletParticle.transform.parent = myTransform;
        }

        if (muzzleParticlePrefab)
        {
            muzzleParticle = Instantiate(muzzleParticlePrefab, myTransform.position, myTransform.rotation);
            muzzleParticle.transform.parent = myTransform;
            Destroy(muzzleParticle, 1.5f);
        }

        StartCoroutine(HandleBulletLifetime(5f));
    }

    IEnumerator HandleBulletLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        if (!destroyed)
        {
            destroyed = true;
            bulletPool.ReturnBullet(gameObject); 
        }
    }

    public void Initialize(float damage, BulletPool pool)
    {
        this.damage = damage;
        this.bulletPool = pool; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyed)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent<CPlayerController>(out CPlayerController playerController))
            {
                playerController.Hit(damage);
            }

            if (!destroyed)
            {
                destroyed = true;
                GameObject impactP = Instantiate(impactParticle, myTransform.position, Quaternion.identity);
                Destroy(impactP, 5.0f);
                bulletPool.ReturnBullet(gameObject);
            }
        }
        else if(other.gameObject.CompareTag("Untagged"))
        {
            if (!destroyed)
            {
                destroyed = true;
                GameObject impactP = Instantiate(impactParticle, myTransform.position, Quaternion.identity);
                Destroy(impactP, 5.0f);
                bulletPool.ReturnBullet(gameObject);
            }
        }
    }

    private void OnDisable()
    {
        // Null 체크를 통해 bulletPool이 null일 경우 처리
        if (bulletPool != null && !destroyed)
        {
            destroyed = true;
            bulletPool.ReturnBullet(gameObject);
        }
    }
}
