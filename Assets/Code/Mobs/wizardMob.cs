using UnityEngine;

public class wizardMob : MobBase
{
    [SerializeField] private float _detectionRange = 8f;
    [SerializeField] private float _keepDistance = 5f;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private float _projectileSpeed = 6f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;

    private float _nextAttackTime;

    private void FixedUpdate()
    {
        Move();
        Attack();
    }

    public override void Move()
    {
        if (_player == null) return;

        float distance = Vector2.Distance(transform.position, _player.position);
        float direction = Mathf.Sign(_player.position.x - transform.position.x);

        if (distance > _detectionRange)
        {
            _rb.linearVelocityX = 0f;
        }
        else if (distance < _keepDistance - 0.5f)
        {
            _rb.linearVelocityX = -direction * _velocity; // se aleja
        }
        else if (distance > _keepDistance + 0.5f)
        {
            _rb.linearVelocityX = direction * _velocity;  // se acerca
        }
        else
        {
            _rb.linearVelocityX = 0f;
        }
    }

    public override void Attack()
    {
        if (_player == null || Time.time < _nextAttackTime) return;
        if (Vector2.Distance(transform.position, _player.position) > _detectionRange) return;

        _nextAttackTime = Time.time + _attackCooldown;

        Vector2 direction = _player.position - _firePoint.position;
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        projectile.GetComponent<EnemyProjectile>().Init(direction, _baseDamage, _projectileSpeed);
    }
}