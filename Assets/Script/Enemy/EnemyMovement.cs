using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    [SerializeField] float range = 15f;
    private string playerTag = "Player";
    
    PlayerHealth playerHealth;
    NavMeshAgent nav;

    Animator anim;

    void Awake()
    {        
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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
        if (playerHealth.currentHealth > 0)
        {
        Move();
        }
        else
        {
            nav.enabled = false;
        Idle();
        }

    }

    void Move()
    {
        nav.SetDestination(target.position);
        MoveAnimation();
    }

    void Idle()
    {
        anim.SetBool("PlayerFound", false);
    }

    void MoveAnimation()
    {
        anim.SetBool("PlayerFound", true);
    }

    private void OnDrawGizmosSelected() //permite ver en el editor el rastreo de los personajes de updatetarget()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}

