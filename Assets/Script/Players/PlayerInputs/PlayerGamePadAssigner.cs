using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerGamePadAssigner : MonoBehaviour
{
    private PlayerInputManager _inputManager;

    [Header("Player 1")]
    [SerializeField] private PlayerInput _playerInput1;

    [Header("Player 2")]
    [SerializeField] private PlayerInput _playerInput2;


    private void Awake()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        _inputManager.onPlayerJoined += CheckInputs;
    }

    private void Start()
    {
        /*
        Debug.Log(Gamepad.all.Count);
        if (Gamepad.all.Count > 1)
        {
            _playerInput1.user.UnpairDevices();
            _playerInput2.user.UnpairDevices();

            InputUser.PerformPairingWithDevice(Gamepad.all[0], _playerInput1.user);
            InputUser.PerformPairingWithDevice(Gamepad.all[1], _playerInput2.user);
        }else if (Gamepad.all.Count == 1)
        {
            _playerInput1.user.UnpairDevice(Gamepad.all[0]);
            _playerInput2.user.UnpairDevices();
            InputUser.PerformPairingWithDevice(Gamepad.all[0], _playerInput2.user);
        }*/
    }

    public void CheckInputs(PlayerInput _playerInput)
    {
        
        if (Gamepad.all.Count == 1)
        {
            if (_playerInput.gameObject.name == "Levana_Animated")
            {
                _playerInput.user.UnpairDevice(Gamepad.all[0]);
            }
        }

        if (Gamepad.all.Count == 2)
        {
            if (_playerInput.gameObject.name == "Levana_Animated")
            {
                _playerInput.user.UnpairDevices();
                InputUser.PerformPairingWithDevice(Gamepad.all[0], _playerInput.user);
            }
            
            if (_playerInput.gameObject.name == "Soleil_Animated")
            {
                _playerInput.user.UnpairDevices();
                InputUser.PerformPairingWithDevice(Gamepad.all[1], _playerInput.user);
            }
        }
    }
}
