using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] int attackDamage = 10;

    Animator anim;

    bool levanaInRange,soleilInRange;
    float timer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerStay(Collider enemy)
    {
        GameObject enemyobject = enemy.gameObject;
        if (enemyobject.CompareTag("Player") & timer >= timeBetweenAttacks)
        {
            timer = 0;
            PlayerHealth playerhealth = enemyobject.GetComponent<PlayerHealth>();
            playerhealth.TakeDamage(attackDamage);
            if (playerhealth.currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }
    }
}
