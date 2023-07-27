using UnityEngine;

public class ProyectileAttack : MonoBehaviour
{
    [SerializeField] int attackDamage = 20;
    void Start()
    {
        Destroy(gameObject, 5f); //Lifetime
    }
    private void OnTriggerEnter(Collider enemy)
    {
        GameObject enemyobject = enemy.gameObject;
        if (enemyobject.CompareTag("Player"))
        {
            PlayerHealth playerhealth = enemyobject.GetComponent<PlayerHealth>();
            playerhealth.TakeDamage(attackDamage);
            Destroy(this.gameObject);
        }
    }

}
