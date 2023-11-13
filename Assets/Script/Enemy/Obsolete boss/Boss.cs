using System.Collections.Generic;
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
    
    [SerializeField] float fireRate = 1f;
    private float fireCountdown = 0f;
    
    
    //configuracion de guardianes
    private bool haveGuardians;
    private bool haveSummonGuardians;
    
    private float vulnerableTime = 10f;
    private float vulnerableTimer;

    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject assassinPrefab;

    private List<GameObject> aliveGuardians;

    [SerializeField] private List<GameObject> guardiansSpawnPoints;
    
    [SerializeField] private GameObject _spawnParticles;

    [SerializeField] private List<ParticleSystem> invulnerableParticles;


    [SerializeField] private AudioSource summonVoice;
    [SerializeField] private AudioSource summonSound;
    [SerializeField] private AudioSource FireBall;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        Healthbar.gameObject.SetActive(false);
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        aliveGuardians = new List<GameObject>();
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

       // Healthbar.value = enemyHealth.currentHealth;

        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        Move();


        CheckForGuardians();
        
        
        float targetDistance = Vector3.Distance(transform.position, target.transform.position); //1
        if (fireCountdown <= 0f & targetDistance <= stoppingDistance && enemyHealth.currentHealth > 0)
        {
            ShootingAnimation();         
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;    
    }


    public void Move()
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        if (targetDistance > stoppingDistance)
        {
            Restart();
            nav.SetDestination(target.position);
            Healthbar.gameObject.SetActive(true);
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


    private void CheckForGuardians()
    {
        if (!haveGuardians)
        {
            enemyHealth.isInmune = false;
            if (vulnerableTimer > 0)
            {
                vulnerableTimer -= Time.deltaTime;
            }
            else if(!haveSummonGuardians)
            {
                SpawnEnemyAnimation();
                vulnerableTimer = vulnerableTime;
            }
        }
    }
    
    public void SummonGuardians()
    {
        //invoca enemigos entre tanques soldados y asesinos
        enemyHealth.isInmune = true;
        haveGuardians = true;
        for (int i = 0; i < invulnerableParticles.Count; i++)
        {
            invulnerableParticles[i].Play();
        }
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                SpawnEnemy(soldierPrefab, guardiansSpawnPoints[0]);
                SpawnEnemy(soldierPrefab, guardiansSpawnPoints[1]);
                SpawnEnemy(soldierPrefab, guardiansSpawnPoints[2]);
                break;
            case 1:
                SpawnEnemy(tankPrefab, guardiansSpawnPoints[0]);
                SpawnEnemy(tankPrefab, guardiansSpawnPoints[1]);
                break;
            case 2:
                SpawnEnemy(assassinPrefab, guardiansSpawnPoints[0]);
                SpawnEnemy(assassinPrefab, guardiansSpawnPoints[1]);
                SpawnEnemy(assassinPrefab, guardiansSpawnPoints[2]);
                break;
        }
        summonSound.Play();
    }
    
    private void SpawnEnemy(GameObject enemyPrefab, GameObject spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        GameObject particles = Instantiate(_spawnParticles, spawnPoint.transform.position, Quaternion.identity);
        aliveGuardians.Add(enemy);
        enemy.AddComponent<WaveEnemy>().OnDeath += DetectGuardianDeath;
        Destroy(particles, 2f);
    }

    private void DetectGuardianDeath(WaveEnemy enemy)
    {
        if (aliveGuardians.Contains(enemy.gameObject))
        {
            aliveGuardians.Remove(enemy.gameObject);
        }

        if (aliveGuardians.Count == 0)
        {
            haveGuardians = false;
            haveSummonGuardians = false;
            for (int i = 0; i < invulnerableParticles.Count; i++)
            {
                invulnerableParticles[i].Stop();
            }
        }
        
    }
    
    

    public void Shoot()
    {
        Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        FireBall.Play();
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
        haveGuardians = true;
        summonVoice.Play();
        anim.SetTrigger("Spawning");
    }

    public void SpawnEnemy()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        randomEnemy = Random.Range(0, enemies.Length);
        Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);

    }


    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seekingRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }

}



