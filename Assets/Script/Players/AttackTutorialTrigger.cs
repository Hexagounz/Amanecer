using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _attackTutorialText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _attackTutorialText.SetActive(true);
            Destroy(gameObject, 3f);
        }
    }


    private void OnDestroy()
    {
        _attackTutorialText.SetActive(false);
    }
}
