using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public int attackDamage = 20; //publico por si se desea modificar con pickups
    [SerializeField] float speed;
    private Rigidbody rb;
    
    EnemyHealth enemyHealth;

   [Header("VFX")]

    [SerializeField] GameObject HitPrefab;
    [SerializeField] GameObject ReflecPrefab;


    [SerializeField] private GameObject explosionPrefab;

    bool enemyInRange;

    GameObject Self;

    private void Awake()
    {
        Self = this.gameObject;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyHealth = other.GetComponent<EnemyHealth>();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Attack();
        }

        else if (other.tag == "EnemyShield")
        {
            speed = -10;
            Self.transform.forward = -transform.forward;
            rb.velocity = -transform.forward * speed;
        }
    }

    public void Attack()
    {
        if (enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(attackDamage);
        }
        Destroy(gameObject);
    }
}
