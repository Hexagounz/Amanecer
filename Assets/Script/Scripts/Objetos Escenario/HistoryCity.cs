using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryCity : MonoBehaviour
{
    [SerializeField] private GameObject dialog;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialog.SetActive(true);
            Destroy(dialog, 8f);
            Destroy(this);
        }
    }
}
