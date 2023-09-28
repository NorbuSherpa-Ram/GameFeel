using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity 
{
    public static PlayerController Instance;

    [Header("Component")]
    private PlayerInput playerInput;
    public  FrameInput frameInput { get; private set; }


    [Header("Ground_Info")]
    [SerializeField] private Transform feetPosition;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask whatIsGround;


    private bool hasDoubleJump;
    public static Action OnJump;

    [Header("Custom Gravity ")]
    [SerializeField] private float customGravity = 700;
    [SerializeField] private float gravityTime = .2f;
    [SerializeField] private float coyoteTime = 2f;
    private float airTimer, coyoteTimer;

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null) { Instance = this; }
        playerInput = GetComponent<PlayerInput>();
    }


    private void OnEnable()
    {
        OnJump += ApplyJumpForce;
    }
    private void OnDisable()
    {
        OnJump -= ApplyJumpForce;
    }
    protected   override  void Start()
    {
        base.Start();
    }
    protected override  void Update()
    {
        GatherInput();
        Jump();
        CoyoteTimer();
        HandleSpriteFlip();
        OnAirTimer();
    }
    protected override   void FixedUpdate()
    {
        Move();
        CustomGravity();
    }
    private void GatherInput()
    {
        if(!isKnockback)
        frameInput = playerInput.frameInput;
    }

    private void Move()
    {
        SetVelocity(frameInput.Move.x * moveSpeed, myRb.velocity.y);
    }

    #region EXTRA_GRAVITY
    private void OnAirTimer()
    {
        if (!IsGrounded())
            airTimer += Time.deltaTime;
        else
            airTimer = 0;
    }
    private void CustomGravity()
    {
        if (airTimer > gravityTime)
            myRb.AddForce(Vector2.down * customGravity * Time.deltaTime);
    }
    #endregion 

    #region JUMP_LOGIC
    private void Jump()
    {
        if (!frameInput.Jump) return;

        if (IsGrounded())
        {
            OnJump?.Invoke();
            //coyoteTimer = -.5f;
        }
        else if (coyoteTimer >= 0)
            OnJump?.Invoke();
        else if (hasDoubleJump)
        {
            airTimer = 0;
            hasDoubleJump = false;
            OnJump?.Invoke();
        }
    }

    private void CoyoteTimer()
    {
        if (IsGrounded())
        {
            hasDoubleJump = true;
            coyoteTimer = coyoteTime;
        }
        else
            coyoteTimer -= Time.deltaTime;
    }
    private void ApplyJumpForce()
    {
        coyoteTimer = 0;
        myRb.velocity = Vector2.zero;
        myRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    #endregion 

    public bool IsGrounded() => Physics2D.OverlapBox(feetPosition.position, groundCheckSize, 0, whatIsGround);

    private void HandleSpriteFlip()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingDirection = -1;
        }
        else
        {
            facingDirection = 1;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow ;
        Gizmos.DrawWireCube(feetPosition.position, groundCheckSize);
    }
}
