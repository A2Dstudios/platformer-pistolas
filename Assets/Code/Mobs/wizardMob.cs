
using UnityEngine;

public class wizardMob : MobBase
{
    [Header("Wizard")]
    [SerializeField] private float _detectionRange = 8f;
    [SerializeField] private float _keepDistance = 5f;
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
        if (_player == null || _rb == null)
        {
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            _player.position
        );

        float direction = Mathf.Sign(
            _player.position.x - transform.position.x
        );

        if (distance > _detectionRange)
        {
            _rb.linearVelocityX = 0f;
        }
        else if (distance < _keepDistance - 0.5f)
        {
            _rb.linearVelocityX = -direction * _velocity;
        }
        else if (distance > _keepDistance + 0.5f)
        {
            _rb.linearVelocityX = direction * _velocity;
        }
        else
        {
            _rb.linearVelocityX = 0f;
        }
    }

    public override void Attack()
    {
        if (_player == null ||
            _projectilePrefab == null ||
            _firePoint == null)
        {
            return;
        }

        if (Time.time < _nextAttackTime)
        {
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            _player.position
        );

        if (distance > _detectionRange)
        {
            return;
        }

        _nextAttackTime = Time.time + _attackCooldown;

        Vector2 direction =
            (Vector2)_player.position -
            (Vector2)_firePoint.position;

        GameObject projectile = Instantiate(
            _projectilePrefab,
            _firePoint.position,
            Quaternion.identity
        );

        EnemyProjectile enemyProjectile =
            projectile.GetComponent<EnemyProjectile>();

        if (enemyProjectile != null)
        {
            enemyProjectile.Init(
                direction,
                _baseDamage,
                _projectileSpeed
            );
        }
        else
        {
            Debug.LogError(
                "El prefab necesita el script EnemyProjectile.",
                projectile
            );

            Destroy(projectile);
        }
    }
}
