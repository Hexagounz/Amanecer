using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerGamePadAssigner : MonoBehaviour
{
    private PlayerInputManager _inputManager;

    [SerializeField]
    private PlayerInput _player1;
    [SerializeField]
    private PlayerInput _player2;


    [SerializeField]
    private GameObject[] spawnPoints;

    private void Awake()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        InitializePlayers();
    }


    private void Start()
    {

    }


    private void InitializePlayers()
    {

        
        _player1.enabled = true;
        _player2.enabled = true;

        
    }

    public void CheckPlayers(PlayerInput _player)
    {
        Debug.Log(_player.gameObject.name);
        Debug.Log(_player.user.valid);
        
        if(_player == _player1 && Gamepad.all.Count > 0)
            _player.user.UnpairDevice(Gamepad.current);


    }
}
