
using UnityEngine;

public class zombieMob : MobBase
{
    [Header("Zombie")]
    [SerializeField] private float _detectionRange = 6f;
    [SerializeField] private float _stopDistance = 1.2f;

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

        if (distance > _detectionRange ||
            distance <= _stopDistance)
        {
            _rb.linearVelocityX = 0f;
            return;
        }

        float direction = Mathf.Sign(
            _player.position.x - transform.position.x
        );

        _rb.linearVelocityX = direction * _velocity;
    }

    public override void Attack()
{
    bool haPegado = AttackOnContact();

    if (haPegado)
    protected override void Attack()
    {
        Debug.Log("¡El zombie ha pegado al jugador! Daño: " + _baseDamage);
    }
}
}
