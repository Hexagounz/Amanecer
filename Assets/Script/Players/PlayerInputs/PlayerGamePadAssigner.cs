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
    }

    private void Start()
    {

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
        }


    }
}
