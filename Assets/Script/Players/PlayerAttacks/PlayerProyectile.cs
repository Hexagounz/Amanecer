using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProyectile : MonoBehaviour
{
    [SerializeField] string SearchedTag;
    [SerializeField] int Damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.CompareTag(SearchedTag))
        {
            EnemyHealth enemyHealth = hitObject.GetComponent<EnemyHealth>();
            enemyHealth.currentHealth -= Damage;            
            Destroy(this.gameObject);
        }
    }
}
