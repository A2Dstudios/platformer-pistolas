
using UnityEngine;

public class batMob : MobBase
{
    [Header("Movement")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionRange = 6f;
    [SerializeField] private float _stopDistance = 1.5f;
    [SerializeField] private float _patrolDistance = 3f;

    [Header("Melee Attack")]
    [SerializeField] private float _attackCooldown = 1f;

    private Vector2 _startPosition;
    private int _patrolDirection = 1;

    private float _nextAttackTime;
    private PlayerHealth _playerInContact;

    protected new void Awake()
    {
        base.Awake();
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        if (_rb == null)
        {
            return;
        }

        if (_player == null)
        {
            Patrol();
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            _player.position
        );

        if (distance <= _detectionRange)
        {
            ChasePlayer(distance);
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        float leftLimit = _startPosition.x - _patrolDistance;
        float rightLimit = _startPosition.x + _patrolDistance;

        if (transform.position.x >= rightLimit)
        {
            _patrolDirection = -1;
        }
        else if (transform.position.x <= leftLimit)
        {
            _patrolDirection = 1;
        }

        _rb.linearVelocity = new Vector2(
            _patrolDirection * _velocity,
            0f
        );
    }

    private void ChasePlayer(float distance)
    {
        if (distance <= _stopDistance)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction =
            ((Vector2)_player.position -
             (Vector2)transform.position).normalized;

        _rb.linearVelocity = direction * _velocity;
    }

    public override void Attack()
    {
        if (_playerInContact == null || Time.time < _nextAttackTime)
        {
            return;
        }

        _playerInContact.TakeDamage(_baseDamage);
        _nextAttackTime = Time.time + _attackCooldown;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckContact(collision.collider);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CheckContact(other);
    }

    private void CheckContact(Collider2D other)
    {
        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
        {
            return;
        }

        _playerInContact = playerHealth;
        Attack();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ClearContact(collision.collider);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ClearContact(other);
    }

    private void ClearContact(Collider2D other)
    {
        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == _playerInContact)
        {
            _playerInContact = null;
        }
    }

    public void AssignPlayer(Transform transformPlayer)
    {
        _player = transformPlayer;
    }
}
