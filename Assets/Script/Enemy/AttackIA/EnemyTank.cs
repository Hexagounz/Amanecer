using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : MonoBehaviour
{

    private Transform target;

    [Header("R Attributes")]

    [SerializeField] float seekingRange = 15f;
    [SerializeField] float stoppingDistance;

    [Header("F. Attributes")]

    [SerializeField] float fireRate = 1f;
    [SerializeField] float fireCountdown = 0f;

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
        //playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

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

        if (nearestEnemy != null && shortestDistance <= seekingRange)
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

        Move();

        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        if (fireCountdown <= 0f & targetDistance <= stoppingDistance && enemyHealth.currentHealth > 0)
        {
            ShootingAnimation();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
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
        else if (targetDistance <= stoppingDistance)
        {
            Stop();
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

    void Shoot() //el tanque se diferencia del soldado debido a que es la animacion la que dicta cuando ocurre el disparo, esto es por la forma precisa en que ocurre dicha animacion.
                 //hacer el calculo de cuando se va a ocurrir el disparo, teniendo en cuenta que el personaje se mueva, que se cambie de objetivo, etc. no es imposible pero si una locura
                // "please understand" nintendo. cuando se contraten nuevos animadores se repara.
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        TankMissile bullet = bulletGO.GetComponent<TankMissile>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }

        if (playerHealth.currentHealth <= 0)
        {
            Stop();
        }
    }

    void Idle() 
    {
        anim.SetTrigger("PlayerMissing");
    }

    void MoveAnimation()
    {
        anim.SetTrigger("PlayerFound");
    }

    void ShootingAnimation()
    {
        anim.SetTrigger("Shooting");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seekingRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
    

}

