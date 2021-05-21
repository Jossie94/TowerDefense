using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    private EnemyMovement targetEnemy;

    [Header("Attributes")]

    public float range = 15f;

    public float fireRate = 1f;

    public float fireCountdown = 0f;

    public GameObject bulletPrefab;


    [Header("Use laser")]
    public bool useLaser = false;

    public LineRenderer lineRenderer;

    public Light impactLight;

    public ParticleSystem impactEffect;

    public int damageovertime = 30;

    public float slowamount = .5f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform RotateTurret;

    public float turnspeed = 10f;

   
    public Transform firePoint;



   
    // Start is called before the first frame update


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject closestenemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestenemy = enemy;
            }
        }

        if (closestenemy != null && shortestDistance <= range)
        {
            target = closestenemy.transform;
            targetEnemy = closestenemy.GetComponent<EnemyMovement>();
        }

        else
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {

            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                   lineRenderer.enabled = false;
                   impactEffect.Stop();
                   impactLight.enabled = false;
                }
            }

            return;

        }
            

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }

        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

        
    }

    void LockOnTarget ()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(RotateTurret.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        RotateTurret.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageovertime * Time.deltaTime);
        targetEnemy.Slow(slowamount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);


        Vector3 direction = firePoint.position - target.position;

        impactEffect.transform.position = target.position + direction.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(direction);
    }

    void Shoot()
    {
        //Debug.Log("Shoot!");
        GameObject bulletboom = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletboom.GetComponent<Bullet>();

        if (bullet != null)
        
            bullet.SearchTarget(target);
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
