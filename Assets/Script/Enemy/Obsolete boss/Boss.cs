using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Transform target;
    EnemyHealth enemyHealth;

    [Header("Teleport Settings")]

    public float xRange;
    public float yRange;
    public float zRange;

    [Header("R Attributes")]

    public float seekingRange = 15f;
    public float stoppingDistance;
 
    [Header("Unity Setup Fields")]

    public string playerTag = "Player";

    public Transform partToRotate;
    public float turnSpeed = 5f;

    public GameObject bulletprefab;
    public Transform firepoint;
    public GameObject Ultprefab;
    public Transform Ultfirepoint;    

    [Header("Spawn Attributes")]

    public Transform[] spawnPoints;
    public GameObject[] enemies;
    int randomSpawnPoint, randomEnemy;

    [Header("Boss Stuff")]

    public Slider Healthbar;

    Animator anim;

    PlayerHealth playerHealth;

    NavMeshAgent nav;

    public bool blasting = true;
    public bool teleporting;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Healthbar.enabled = false;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    public void UpdateTarget() // mover a boss manager
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

        if (nearestEnemy != null && shortestDistance <= seekingRange)
        {
            target = nearestEnemy.transform;
        }
    }

    private void Update()
    {

        Healthbar.value = enemyHealth.currentHealth;

        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        Move();
    }


    public void Move()
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        if (targetDistance > stoppingDistance)
        {
            Restart();
            nav.SetDestination(target.position);
            Healthbar.enabled = true;
            Idle();
        }
        else if (targetDistance <= stoppingDistance)
        {
            Stop();
        }
    }

    public void Stop()    {
        nav.isStopped = true;  }

    public void Restart()    {
        nav.isStopped = false;  }

    public void Shoot()
    {
        Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        if (playerHealth.currentHealth <= 0)
        {
            Stop();
        }
    }

    public void ShootBlast()
    {
        Instantiate(Ultprefab, Ultfirepoint.position, Ultfirepoint.rotation);
        if (playerHealth.currentHealth <= 0)
        {
            Stop();
        }
    }

    public void Idle()
    {
        anim.SetTrigger("PlayerFound");
    }

    public void ShootingBlastAnimation()
    {
        turnSpeed = 1f;
        anim.SetTrigger("Blast");
    }

    public void ShootingAnimation()
    {
        anim.SetTrigger("Shooting");
    }

    public void ShootBlastFinished()
    {
        blasting = false;
        turnSpeed = 5f;
    }

    public void SpawnEnemyAnimation()
    {
        anim.SetTrigger("Spawning");
    }

    public void SpawnEnemy()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        randomEnemy = Random.Range(0, enemies.Length);
        Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);

    }

    public void Teleport()
    {
        float newX = Random.Range(-xRange, xRange);
        float newZ = Random.Range(-zRange, zRange);

        transform.position = new Vector3(newX, yRange, newZ);

        teleporting = false;
    }

    public void TeleportStart()
    {
        teleporting = true;
    }

    public void TeleportFinished()
    {
        teleporting = false;
    }

    public void TeleportAnimation()
    {
        anim.SetTrigger("Teleport");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seekingRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }

}



