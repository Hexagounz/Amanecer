using UnityEngine;

public class MushroomTutorial : MonoBehaviour
{
    EnemyHealth mushroomHealth;
    [SerializeField] GameObject attackTutorial;


    void Start()
    {
        mushroomHealth = gameObject.GetComponent<EnemyHealth>();
    }

   private void OnTriggerEnter(Collider enemy)
    {
        GameObject enemyobject = enemy.gameObject;
        if (enemyobject.CompareTag("Player"))
        {
            attackTutorial.SetActive(true);
        }
    }


    void Update()
    {
        if (mushroomHealth.currentHealth <= 0f)
        {
            attackTutorial.SetActive(false);
        }
    }
}
