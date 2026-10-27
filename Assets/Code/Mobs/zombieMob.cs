using UnityEngine;

public class zombieMob : MobBase
{
    [SerializeField] private float _detectionRange = 6f;
    [SerializeField] private float _stopDistance = 1.5f;

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        if (_player == null || _rb == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, _player.position);

        if (distance <= _stopDistance || distance > _detectionRange)
        {
            _rb.linearVelocityX = 0f;
            return;
        }

        float direction = Mathf.Sign(_player.position.x - transform.position.x);

        _rb.linearVelocityX = direction * _velocity;
    }

    public override void Attack()
    {
        // Pendiente
    }
}