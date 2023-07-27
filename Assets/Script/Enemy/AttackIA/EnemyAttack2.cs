using UnityEngine;

public class EnemyAttack2 : MonoBehaviour {
    //version antigua del ataque
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject Soleil_Animated;
    PlayerHealth playerHealth;

    bool playerInRange;
    float timer;

    private void Awake()
    {
        Soleil_Animated = GameObject.Find("Soleil_Animated");
        playerHealth = Soleil_Animated.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Soleil_Animated)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Soleil_Animated)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
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
}
