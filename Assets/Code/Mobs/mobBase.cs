
using System;
using UnityEngine;

public abstract class MobBase : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected float _maxHP = 3f;
    [SerializeField] protected float _velocity = 2f;
    [SerializeField] protected float _baseDamage = 1f;
    [SerializeField] protected int _points = 10;

    [Header("Contact Attack")]
    [SerializeField] protected float _attackRange = 1.2f;
    [SerializeField] protected float _attackCooldown = 1f;

    protected float _hP;
    protected Rigidbody2D _rb;
    protected Transform _player;

    private float _nextContactAttackTime;
    private bool _isDead;

    public event Action<int> SendToScore;

    protected virtual void Awake()
    {
        _hP = _maxHP;
        _rb = GetComponent<Rigidbody2D>();

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            SetPlayer(playerObject.transform);
        }
        else
        {
            Debug.LogWarning(
                "No se encuentra ningún objeto con la tag Player.",
                this
            );
        }
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    public void TakeDamage(float quantity)
    {
        if (_isDead || quantity <= 0f)
        {
            return;
        }

        _hP -= quantity;

        if (_hP <= 0f)
        {
            Die();
        }
    }

    protected bool AttackOnContact()
    {
        if (_isDead || _player == null)
        {
            return false;
        }

        if (Time.time < _nextContactAttackTime)
        {
            return false;
        }

        float distance = Vector2.Distance(
            transform.position,
            _player.position
        );

        if (distance > _attackRange)
        {
            return false;
        }

        PlayerHealth playerHealth =
            _player.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            playerHealth =
                _player.GetComponentInParent<PlayerHealth>();
        }

        if (playerHealth == null)
        {
            Debug.LogWarning(
                "No se encuentra PlayerHealth en el jugador.",
                _player
            );

            return false;
        }

        _nextContactAttackTime =
            Time.time + _attackCooldown;

        Debug.Log(
            name + " ataca por contacto. Daño: " + _baseDamage
        );

        playerHealth.TakeDamage(_baseDamage);

        return true;
    }

    public abstract void Move();

    public abstract void Attack();

    private void Die()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;

        SendToScore?.Invoke(_points);

        Destroy(gameObject);
    }
}
