using System;
using UnityEngine;

public abstract class MobBase : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected float _maxHP;
    [SerializeField] protected float _velocity;
    [SerializeField] protected float _baseDamage;
    [SerializeField] protected int _points;
    protected float _hP;

    [SerializeField] protected Transform _player;

    public event Action<int> SendToScore;

    [SerializeField] protected Rigidbody2D _rb;

    private bool _isDead = false;

    protected virtual void Awake()
    {
        _hP = _maxHP;

        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Si nadie le ha asignado el jugador, lo busca por su tag
        if (_player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                _player = playerObject.transform;
            }
        }
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    public void TakeDamage(float quantity)
    {
        if (_isDead || quantity <= 0)
        {
            return;
        }

        _hP -= quantity;

        if (_hP <= 0)
        {
            _hP = 0;
            Die();
        }
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