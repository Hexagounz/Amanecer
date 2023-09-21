using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] int startingHealth = 1000;
    public int currentHealth;
    private float sinkSpeed = 1.5f;

    Animator anim;
    CapsuleCollider capsuleCollider;
    private bool isDead;
    bool isSinking;

    private SkinnedMeshRenderer textureMaterial;

    public bool isInmune;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        textureMaterial = GetComponentInChildren<SkinnedMeshRenderer>();
        currentHealth = startingHealth;
    }

    private void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isInmune)
        {
            currentHealth -= amount;
            StartCoroutine(DamageFlashing());

            if (currentHealth <= 0)
            {
                Death();            
            }
        }
    }

    void Death()
    {
        isDead = true;
        capsuleCollider.enabled = false;
        anim.SetTrigger("Dead");

    }

    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }

    IEnumerator DamageFlashing()
    {
        textureMaterial.material.color = Color.Lerp(Color.white,Color.red, 0.2f);
        yield return new WaitForSeconds(0.2f);
        textureMaterial.material.color = Color.Lerp(Color.red, Color.white, 0.2f);
        yield return new WaitForSeconds(0.2f);
        textureMaterial.material.color = Color.Lerp(Color.white,Color.red, 0.2f);

    }
}
