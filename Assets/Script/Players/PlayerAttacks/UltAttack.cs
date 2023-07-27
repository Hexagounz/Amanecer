using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltAttack : MonoBehaviour {

    [SerializeField] int attackDamage = 200;
    
    EnemyHealth enemyHealth;

    public List<EnemyHealth> TouchingEnemies;

    private void Start()
    {
        TouchingEnemies = new List<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!TouchingEnemies.Contains(enemyHealth))
        {
            enemyHealth = other.GetComponent<EnemyHealth>();
            Attack();
        }
    }

    void Attack()
    {
        if (enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(attackDamage);
        }
    }
}
