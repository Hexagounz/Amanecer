using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemySoldier : MonoBehaviour {

    private Transform target;

    [Header("R Attributes")]

    [SerializeField] float seekingRange = 15f;
    [SerializeField] float stoppingDistance;

    [Header("F. Attributes")]

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
    NavMeshAgent nav;

    void Awake()
    {        
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();

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

        foreach (GameObject enemy in enemies){
            playerHealth = enemy.GetComponent<PlayerHealth>();
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance){
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;}
        }

        if (nearestEnemy != null && shortestDistance <= seekingRange)        {
            target = nearestEnemy.transform;
        }
    }

    private void Update()
    {
        if (target == null)
            return;
    
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir); //Quaternion = datos de 360 grados
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //funciona y (2d)

        Move();

        float targetDistance = Vector3.Distance(transform.position, target.transform.position); //1
        if (fireCountdown <= 0f & targetDistance <= stoppingDistance && enemyHealth.currentHealth > 0)
        {
            Shoot();           
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;    
    }

    void Move()
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position); //1
        if (targetDistance > stoppingDistance)
        {
            Restart();
            nav.SetDestination(target.position);
            MoveAnimation();
        }
        else if (targetDistance <= stoppingDistance)
        {
            Stop();
        }
    }

    void Stop()    {
        nav.isStopped = true;
    }

    void Restart()    {
        nav.isStopped = false;
    }

    void Shoot()
    {
        ShootingAnimation();
        Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        if (playerHealth.currentHealth <= 0)
        {
            Stop();
        }      
    }

    void Idle()
    { //el idle ocurre dentro de las mismas animaciones. tan ironico como eso suene.
        anim.SetTrigger("PlayerMissing");    }

    void MoveAnimation()    {
        anim.SetTrigger("PlayerFound");    }

    void ShootingAnimation()    {
        anim.SetTrigger("Shooting");    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, seekingRange);
    }
}
