using UnityEngine;

public class FlowerBullerAttackL : MonoBehaviour {

    [SerializeField] int attackDamage = 20;

    GameObject Levana_Animated;
    PlayerHealth playerHealth;

    bool playerInRange; 

    GameObject Self;

    private void Awake()
    {
        Levana_Animated = GameObject.Find("Levana_Animated");
        playerHealth = Levana_Animated.GetComponent<PlayerHealth>();
        Self = gameObject;
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
        if (playerInRange)
        {
            Attack();
        }
        if (playerHealth.currentHealth <= 0)
        {
            Self.SetActive(false);
        }
    }

    void Attack()
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
            Self.SetActive(false);
        }
    }
}
