using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    Boss boss;

    EnemyHealth enemyHealth;

    [Header("Teleport Settings")]

    public float TeleportRate = 2;
    private float TeleportCountdown = 0f;

    [Header("F. Attributes")]

    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float BlastfireRate = 20f;
    private float BlastfireCountdown = 0f;

    [Header("Spawn Attributes")]

    public float SpawnRate = 1f;
    private float SpawnRateCountdown = 0f;

    private void Start()
    {   
        boss = GetComponent<Boss>();
    }

    private void Update()
    {
        BlastfireCountdown -= Time.deltaTime;
        fireCountdown -= Time.deltaTime;
        TeleportCountdown -= Time.deltaTime;
        SpawnRateCountdown -= Time.deltaTime;


        float targetDistance = Vector3.Distance(transform.position, boss.target.transform.position);

        if (boss.teleporting == true)
        {
            boss.Teleport();
        }

        if (BlastfireCountdown <= 0f & targetDistance <= boss.stoppingDistance && enemyHealth.currentHealth > 0)
        {
            boss.ShootingBlastAnimation();
            BlastfireCountdown = 1f / BlastfireRate;
        }

        if (TeleportCountdown <= 0f && enemyHealth.currentHealth > 0)
        {
            boss.TeleportAnimation();
            TeleportCountdown = 1f / TeleportRate;
        }

        if (SpawnRateCountdown <= 0f & targetDistance <= boss.stoppingDistance && enemyHealth.currentHealth > 0)
        {
            boss.SpawnEnemy();
            SpawnRateCountdown = 1f / SpawnRate;
        }

        if (fireCountdown <= 0f & targetDistance <= boss.stoppingDistance && enemyHealth.currentHealth > 0 && boss.blasting == false)
        {
            boss.ShootingAnimation();
            fireCountdown = 1f / fireRate;
        }

    }
}
