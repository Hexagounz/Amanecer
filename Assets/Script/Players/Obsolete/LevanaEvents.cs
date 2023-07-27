using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevanaEvents : MonoBehaviour
{

    public GameObject treant;
    public GameObject movableRocks;

    private void Awake()
    {
        //winText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            other.gameObject.SetActive(false);
            treant.gameObject.SetActive(true);
            movableRocks.gameObject.SetActive(false);
        }
        ///if (other.gameObject.CompareTag("NextLevel"))
        ///{
        ///winText.enabled = true;
        ///}
    }
}
