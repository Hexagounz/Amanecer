using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityWaveDetector : MonoBehaviour
{
    private bool levanaIn;

    private bool soleilIn;

    [SerializeField] private CityWaveController _controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Soleil_Animated")
        {
            soleilIn = true;
        }
        
        if (other.name == "Levana_Animated")
        {
            levanaIn = true;
        }

        if (soleilIn && levanaIn)
        {
            _controller.StartWaves();
            Destroy(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Soleil_Animated")
        {
            soleilIn = false;
        }
        
        if (other.name == "Levana_Animated")
        {
            levanaIn = false;
        }
    }
}
