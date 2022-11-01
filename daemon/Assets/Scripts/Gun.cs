using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f; //Bullet Damage

    public float range = 100f; //Range of Bullet

    public float fireRate = 15f; //Fire rate

    public float impactForce = 30f; //Force of impact

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (
            Input.GetButtonDown("Fire1") && /* replace GetButton with GetButtonDown to change into tap mode*/
            Time.time >= nextTimeToFire
        )
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (
            Physics
                .Raycast(fpsCam.transform.position,
                fpsCam.transform.forward,
                out hit,
                range)
        )
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage (damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO =
                Instantiate(impactEffect,
                hit.point,
                Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
