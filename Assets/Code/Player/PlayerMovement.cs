using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;

    [Header("Jump")]
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _coyoteTime;
    [SerializeField] private float _jumpBuffer;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Refs")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _feetPos;
    [SerializeField] private Animator _animator;

    private PlayerInputs _inputs;
    private Vector2 _moveInput;

    private bool _isGrounded;
    private float _coyoteTimer;
    private float _jumpBufferTime;

    void Awake()
    {
        _inputs = new PlayerInputs();

        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_animator == null) _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputs.Player.Enable();
        _inputs.Player.Action.performed += JumpPressed;
    }
    private void OnDisable()
    {
        _inputs.Player.Disable();
        _inputs.Player.Action.performed -= JumpPressed;
    }

    void Update()
    {
        _moveInput = _inputs.Player.Move.ReadValue<Vector2>();

        HandleAnimations();
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGrounded();

        MovePlayer();
        HandleJumping();
    }

    private bool CheckGrounded()
    {
        return Physics2D.OverlapBox(_feetPos.position, new Vector2(0.5f, 0.1f), 0f, _groundLayer);
    }

    private void MovePlayer()
    {
        float currentVel = _rb.linearVelocityX;
        float targetVel = _moveInput.x * _moveSpeed;

        // mas aceleracion al girar o frenar para mejor movimiento
        bool isTurning = _moveInput.x * currentVel < 0;
        float turnMult = isTurning ? 2f : 1f;

        float newVel = Mathf.MoveTowards(currentVel, targetVel, _acceleration * turnMult * Time.fixedDeltaTime);

        _rb.linearVelocityX = newVel;
    }

    private void HandleJumping()
    {
        if (_isGrounded) _coyoteTimer = _coyoteTime;
        else _coyoteTimer -= Time.fixedDeltaTime;

        bool canJump = _coyoteTimer > 0;
        bool wantsToJump = Time.time < _jumpBufferTime;

        if (canJump && wantsToJump)
        {
            Jump();

            _jumpBufferTime = 0f;
            _coyoteTimer = 0;
        }
    }

    private void Jump()
    {
        _rb.linearVelocityY = _jumpSpeed;
    }

    private void JumpPressed(InputAction.CallbackContext context)
    {
        _jumpBufferTime = Time.time + _jumpBuffer;
    }

    private void HandleAnimations()
    {
        _animator.SetBool("grounded", _isGrounded);
        _animator.SetBool("moving", Mathf.Abs(_moveInput.x) > 0);
        _animator.SetFloat("walkSpeed", Mathf.Abs(_rb.linearVelocityX) / _moveSpeed);

        // flipear el sprite hacia donde te estés moviendo o hacia donde estés disparando
        float aiming= _inputs.Player.Shoot.ReadValue<Vector2>().x;
        if(Mathf.Abs(aiming) > 0)
        {
            if (aiming > 0) transform.localScale = new Vector3(1f, 1f, 1f);
            if (aiming < 0) transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            if (_moveInput.x > 0) transform.localScale = new Vector3(1f, 1f, 1f);
            if (_moveInput.x < 0) transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
