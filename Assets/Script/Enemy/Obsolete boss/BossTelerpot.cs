using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTelerpot : MonoBehaviour {

    public float xRange;
    public float yRange;
    public float zRange;


    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "PlayerBullet")
        {
            Teleport();
        }
    }

    void Teleport()
    {
        float newX = Random.Range(-xRange, xRange);
        float newZ = Random.Range(-zRange, zRange);

        transform.position = new Vector3(newX, yRange, newZ);
    }
}
