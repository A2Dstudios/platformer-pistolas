
using UnityEngine;

public class zombieMob : MobBase
{
    [Header("Movement")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionRange = 6f;
    [SerializeField] private float _stopDistance = 1.5f;

    [Header("Melee Attack")]
    [SerializeField] private float _attackCooldown = 1f;

    private float _nextAttackTime;
    private PlayerHealth _playerInContact;

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

        float distance = Vector2.Distance(
            transform.position,
            _player.position
        );

        if (distance <= _stopDistance || distance > _detectionRange)
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
        if (_playerInContact == null || Time.time < _nextAttackTime)
        {
            return;
        }

        Debug.Log("¡El zombie está atacando al jugador!");

        Debug.Log("Daño que intenta hacer: " + _baseDamage);

        _playerInContact.TakeDamage(_baseDamage);

        Debug.Log("El zombie ha llamado a TakeDamage().");

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

        if (_playerInContact == null)
        {
            Debug.Log("¡El zombie ha detectado contacto con el jugador!");
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
            Debug.Log("El jugador se ha alejado del zombie.");
            _playerInContact = null;
        }
    }

    public void AssisgnPlayer(Transform transformPlayer)
    {
        _player = transformPlayer;

        Debug.Log("Zombie: jugador asignado correctamente.");
    }
}
