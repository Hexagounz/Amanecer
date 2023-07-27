using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUltAttack : MonoBehaviour {

    public int attackDamage = 50;

    PlayerHealth playerHealth;

    public List<PlayerHealth> TouchingEnemies;

    private void Start()
    {
        TouchingEnemies = new List<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!TouchingEnemies.Contains(playerHealth))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            Attack();
        }
    }

    void Attack()
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
