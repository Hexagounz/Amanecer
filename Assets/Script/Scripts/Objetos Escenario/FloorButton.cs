using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private GameObject _activeLigth;
    private bool _isActived;

    public bool IsActived => _isActived;

    public Action OnActive;

    void Start()
    {
        _activeLigth.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _activeLigth.SetActive(true);
            _isActived = true;
            OnActive?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _activeLigth.SetActive(false);
            _isActived = false;
            OnActive?.Invoke();
        }
    }
}
