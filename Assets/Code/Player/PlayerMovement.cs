using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;

    [Header("Refs")]
    [SerializeField] private Rigidbody2D _rb;

    private PlayerInputs _inputs;
    private Vector2 _moveInput;

    void Awake()
    {
        _inputs = new PlayerInputs();

        if (_rb == null) _rb = GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        _inputs.Player.Enable();
    }
    private void OnDisable()
    {
        _inputs.Player.Disable();
    }

    void Update()
    {
        _moveInput = _inputs.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        float currentVel = _rb.linearVelocityX;
        float targetVel = _moveInput.x * _moveSpeed;
        float newVel = Mathf.MoveTowards(currentVel, targetVel, _acceleration * Time.fixedDeltaTime);

        _rb.linearVelocityX = newVel;
    }
}
