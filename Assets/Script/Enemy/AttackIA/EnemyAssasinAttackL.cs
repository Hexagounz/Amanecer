using UnityEngine;
using UnityEngine.AI;

public class EnemyAssasinAttackL : MonoBehaviour
{

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject Levana_Animated;
    PlayerHealth playerHealth;
    NavMeshAgent nav;

    bool playerInRange;
    float timer;

    private void Awake()
    {
        Levana_Animated = GameObject.Find("Levana_Animated");
        playerHealth = Levana_Animated.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Levana_Animated)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Levana_Animated)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            AttackAnimation();
            Attack();
        }
        if (playerHealth.currentHealth <= 0)
        {
            Stop();
        }

    }

    void Attack()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void AttackAnimation()
    {
        anim.SetTrigger("Attacking");
    }

    void Stop()
    {
        nav.isStopped = true;
    }
}
