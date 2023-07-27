using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    [Header("F. Attributes")]

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject bulletprefab;

    public Transform Self;

    void Update()
    {             
        if (Input.GetButton("LFire") && fireCountdown <= 0f)
        {          
            Shoot();
            fireCountdown = 1f / fireRate;
        }

    }
    void Shoot()
    {
        //ShootingAnimation();
        Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
       
    }

}
