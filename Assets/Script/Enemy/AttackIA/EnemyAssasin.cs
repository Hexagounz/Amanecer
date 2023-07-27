using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAssasin : MonoBehaviour
{
    Transform target;
    
    [Header("Attributes")]

    [SerializeField] float timeBetweenAttacks = 0.5f;
    [SerializeField] int attackDamage = 10;
    [SerializeField] float range = 15f;
    [SerializeField] float stoppingDistance;

    [Header("Unity Setup Fields")]

    [SerializeField] string playerTag = "Player";

    [SerializeField] Transform partToRotate;
    [SerializeField] float turnSpeed = 5f;

    Animator anim;

    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;

    public List<PlayerHealth> TouchingEnemies;

    bool playerInRange;
    float timer;


    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    private void Start()
    {
        TouchingEnemies = new List<PlayerHealth>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (!TouchingEnemies.Contains(playerHealth))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!TouchingEnemies.Contains(playerHealth))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerInRange = false;
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

        Move();

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Stop();
            Attack();            
            AttackAnimation();
        }
    }



    void Move()
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        if (targetDistance > stoppingDistance)
        {
            Restart();
            nav.SetDestination(target.position);
            MoveAnimation();
        }

    }

    void Stop()
    {
        nav.isStopped = true;
    }

    void Restart()
    {
        nav.isStopped = false;
    }

    void Attack()
    {
        playerHealth.TakeDamage(attackDamage);
    }

    void Idle()
    {
        anim.SetTrigger("PlayerMissing");
    }

    void MoveAnimation()
    {
        anim.SetTrigger("PlayerFound");
    }

    void AttackAnimation()
    {
        anim.SetTrigger("Attacking");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}
