using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TwoButtonsDoor : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private FloorButton _buttonA;
    [SerializeField] private FloorButton _buttonB;

    private Animator _anim;
    private NavMeshObstacle _navMeshObstacle;
    [SerializeField] private BoxCollider _doorCollider;

    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _buttonA.OnActive += CheckConditions;
        _buttonB.OnActive += CheckConditions;
        _doorCollider.enabled = true;
        _anim = GetComponent<Animator>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        _buttonA.OnActive -= CheckConditions;
        _buttonB.OnActive -= CheckConditions;
    }

    private void CheckConditions()
    {
        if (_buttonA.IsActived && _buttonB.IsActived)
        {
            OpenDoor();
            _navMeshObstacle.enabled = false;
            _doorCollider.enabled = false;
        }
    }

    private void OpenDoor()
    {
        _anim.SetTrigger("Open");
        _audioSource.Play();
    }
}
