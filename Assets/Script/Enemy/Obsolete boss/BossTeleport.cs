using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{

    [Header("Teleport Settings")]

    public float xRange;
    public float yRange;
    public float zRange;

    public float TeleportRate = 2;
    private float TeleportCountdown = 0f;

    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    private void Awake()
    {
        //playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();       
    }

    private void Update()
    {
        if (TeleportCountdown <= 0f && enemyHealth.currentHealth > 0)
        {
            Teleport();
            TeleportCountdown = 1f / TeleportRate;
        }
        TeleportCountdown -= Time.deltaTime;

    }

    void Teleport()
    {
        float newX = Random.Range(-xRange, xRange);
        float newZ = Random.Range(-zRange, zRange);

        transform.position = new Vector3(newX, yRange, newZ);
    }

}
