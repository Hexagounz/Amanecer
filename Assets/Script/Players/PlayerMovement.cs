using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float velocity = 7;
    [SerializeField] float turnSpeed = 10;
    [SerializeField] float height = 0.5f;
    [SerializeField] float heightPadding = 0.05f;
    [SerializeField] LayerMask ground;
    [SerializeField] float maxGroundAngle = 120;
    [SerializeField] string vertical = "Vertical", horizontal = "Horizontal";

    Vector2 input;
    float angle;
    float groundAngle;

    Animator anim;
    Rigidbody playerRigidbody;

    Vector3 Forward;
    RaycastHit hitInfo;
    bool grounded;

    bool canMove;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        canMove = true;
    }

    Quaternion targetRotation;
    private void FixedUpdate()
    {
        Animating();
    }

    private void Update()
    {
        GetInput();
        CalculateDirection();
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();
        ApplyGravity();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        Rotate();
        Move();
    }

    void GetInput()
    {
        input.x = Input.GetAxisRaw(horizontal);
        input.y = Input.GetAxisRaw(vertical);

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
        if (canMove == true)
        {
            playerRigidbody.MovePosition(transform.position += Forward * velocity * Time.deltaTime);
        }

    }

    void Animating()
    {
        bool walking = Input.GetAxisRaw(horizontal) != 0f || Input.GetAxisRaw(vertical) != 0f;
        anim.SetBool("IsWalking", walking);
    }

    void CalculateForward()
    {
        if (!grounded)
        {
            Forward = transform.forward;
            return;
        }
        Forward = Vector3.Cross(transform.right, hitInfo.normal);
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
