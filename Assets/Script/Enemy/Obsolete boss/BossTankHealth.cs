using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHealth : MonoBehaviour
{
    public int startingHealth = 800;
    public int currentHealth;
    private float sinkSpeed = 1.5f;

    Animator anim;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
    }

    private void Update()
    {
        if (isSinking)        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;
        currentHealth -= amount;

        if (currentHealth <= 0)        {
            Death();        }
    }

    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        //FindObjectOfType<MenusinGame>().Victory();
    }


    public void StartSinking()    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}
