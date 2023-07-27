using UnityEngine;
using UnityEngine.AI;

public class EnemyAssasinAttack : MonoBehaviour // es simplemente una modificacion del enemy attack con un anim integrado, pero como el hongo no tiene anim para no torturarme
{
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] int attackDamage = 10;

    Animator anim;
    NavMeshAgent nav;
    float timer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
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
            if (playerhealth.currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
            else
            {
                AttackAnimation();
                playerhealth.TakeDamage(attackDamage);
            }


        }
    }

    void AttackAnimation()
    {
        anim.SetTrigger("Attacking");
    }
}
