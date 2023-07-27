using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlower : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]

    [SerializeField] float range = 15f;
    [SerializeField] float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    [SerializeField] string playerTag = "Player";

    [SerializeField] Transform partToRotate;
    [SerializeField] float turnSpeed = 5f;

    [SerializeField] GameObject bulletprefab;
    [SerializeField] Transform firepoint;

    Animator anim;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(playerTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            playerHealth = enemy.GetComponent<PlayerHealth>();
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }

    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);


        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        if (targetDistance < range && fireCountdown <= 0f && enemyHealth.currentHealth > 0)
        {
            Shoot();            
            fireCountdown = 1f / fireRate;
        }
        else if (targetDistance > range)
        {
            Idle();
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        ShootAnimation();
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    void Idle()
    {
        anim.SetBool("PlayerFound", false);
    }

    void ShootAnimation()
    {
        anim.SetBool("PlayerFound", true);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
