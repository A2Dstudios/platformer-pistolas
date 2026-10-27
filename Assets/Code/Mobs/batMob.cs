using UnityEngine;

public class batMob : MobBase
{
    [SerializeField] private float _detectionRange = 6f;
    [SerializeField] private float _stopDistance = 1.5f;

    [SerializeField] private float _patrolDistance = 3f;

    private Vector2 _startPosition;
    private int _patrolDirection = 1;

    protected override void Awake()
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

        float distance = Vector2.Distance(transform.position, _player.position);

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

        _rb.linearVelocity = new Vector2(_patrolDirection * _velocity, 0f);
    }

    private void ChasePlayer(float distance)
    {
        if (distance <= _stopDistance)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)_player.position - (Vector2)transform.position).normalized;

        _rb.linearVelocity = direction * _velocity;
    }

    protected override void Attack()
    {
        // Pendiente
    }
}