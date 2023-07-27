using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBulletAttackS : MonoBehaviour
{

    [SerializeField] int attackDamage = 20;

    GameObject Soleil;
    PlayerHealth playerHealth;

    bool playerInRange;

    GameObject Self;

    private void Awake()
    {
        Soleil = GameObject.Find("Soleil_Animated");
        playerHealth = Soleil.GetComponent<PlayerHealth>();
        Self = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Soleil)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Soleil)
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