using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float velocity = 7;
    [SerializeField] float turnSpeed = 10;
    [SerializeField] float height = 0.5f;
    [SerializeField] float heightPadding = 0.05f;
    [SerializeField] LayerMask ground;
    [SerializeField] float maxGroundAngle = 120;

    [Header("Inputs")]
    [SerializeField] private PlayerNumber playerNumber = PlayerNumber.player1;
    private PlayerControls controls;

    Vector2 input;
    float angle;
    float groundAngle;

    Animator anim;
    Rigidbody playerRigidbody;

    Vector3 Forward;
    RaycastHit hitInfo;
    bool grounded;

    bool canMove;
    private PlayerInput _playerInput;
    
    private PlayerAbilities _playerAbilities;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        
        _playerAbilities = GetComponent<PlayerAbilities>();
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        canMove = true;
        
        controls = new PlayerControls();

        //EnableAndConfigPlayerInputs();

    }
    
    //Inputs 

    void EnableAndConfigPlayerInputs()
    {
        controls.Player1.Movement.performed += GetMovementInput;
        controls.Player1.Movement.canceled += ctx => input = Vector2.zero;
        controls.Player1.BasicAttack.started += ctx => _playerAbilities.BasicShoot();
        controls.Player1.UltimateAttack.started += ctx => _playerAbilities.DefinitiveShoot();
        controls.Player1.Heal.started += ctx => _playerAbilities.PerformHeal();
            
        controls.Player1.Enable();
        
        /*
        if (playerNumber == PlayerNumber.player1)
        {
            controls.Player1.Movement.performed += GetMovementInput;
            controls.Player1.Movement.canceled += ctx => input = Vector2.zero;
            controls.Player1.BasicAttack.started += ctx => _playerAbilities.BasicShoot();
            controls.Player1.UltimateAttack.started += ctx => _playerAbilities.DefinitiveShoot();
            controls.Player1.Heal.started += ctx => _playerAbilities.PerformHeal();
            
            controls.Player1.Enable();
        }
        else
        {
            controls.Player2.Movement.performed += GetMovementInput;
            controls.Player2.Movement.canceled += ctx => input = Vector2.zero;
            controls.Player2.BasicAttack.started += ctx => _playerAbilities.BasicShoot();
            controls.Player2.UltimateAttack.started += ctx => _playerAbilities.DefinitiveShoot();
            controls.Player2.Heal.started += ctx => _playerAbilities.PerformHeal();
            controls.Player2.Enable();
        }*/
    }
    
    
    
    public void GetMovementInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    Quaternion targetRotation;
    private void FixedUpdate()
    {
        Animating();
    }

    private void Start()
    {
        ButtonsRute();
    }

    private void Update()
    {
        ControlsRute();
        
        CalculateDirection();
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();
        //ApplyGravity();

        if (Mathf.Abs(input.x) == 0 && Mathf.Abs(input.y) == 0) return;

        Rotate();
        Move();
    }
    

    private void ControlsRute()
    {
        if (playerNumber == PlayerNumber.player2)
        {
            input = _playerInput.actions["Movement1"].ReadValue<Vector2>();
        }
        else
        {
            input = _playerInput.actions["Movement"].ReadValue<Vector2>();
        }
    }

    private void ButtonsRute()
    {
        if (playerNumber == PlayerNumber.player2)
        {
            _playerInput.actions["BasicAttack1"].started += ctx => _playerAbilities.BasicShoot();
            _playerInput.actions["UltimateAttack1"].started += ctx => _playerAbilities.DefinitiveShoot();
            _playerInput.actions["Heal1"].started += ctx => _playerAbilities.PerformHeal();
        }
        else
        {
            _playerInput.actions["BasicAttack"].started += ctx => _playerAbilities.BasicShoot();
            _playerInput.actions["UltimateAttack"].started += ctx => _playerAbilities.DefinitiveShoot();
            _playerInput.actions["Heal"].started += ctx => _playerAbilities.PerformHeal();
        }
        
        

        if(playerNumber == PlayerNumber.player1)
            _playerInput.actions["PauseGame"].canceled += ctx => PauseMenuGame.Instance.ControlPauseMenu();
    }
    
    
    
    
    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
    }

    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

    }

    void Move()
    {
        if (groundAngle >= maxGroundAngle)
        {
            return;
        }
        if (canMove)
        {
            playerRigidbody.MovePosition(transform.position += Forward * (velocity * Time.deltaTime));
            
            
        }

    }

    void Animating()
    {
        bool walking = input.x != 0f || input.y != 0f;
        anim.SetBool("IsWalking", walking);
    }

    void CalculateForward()
    {
        if (!grounded)
        {
            Forward = transform.forward;
            return;
        }
        //Forward = Vector3.Cross(transform.right, hitInfo.normal);
        Forward = new Vector3(input.x, 0, input.y).normalized;
    }

    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }
        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    }
    void CheckGround()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hitInfo, height + heightPadding, ground))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    void ApplyGravity()
    {
        if (!grounded)
        {
            transform.position += Physics.gravity * Time.deltaTime;
        }
    }

    void CannotMove()
    {
        canMove = false;
    }

    void CanMove()
    {
        canMove = true;
    }
}

public enum PlayerNumber
{
    player1,
    player2
}
