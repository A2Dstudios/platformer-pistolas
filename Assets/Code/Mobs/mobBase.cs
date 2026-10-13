
using UnityEngine;

public abstract class MobBase : MonoBehaviour
{

    [SerializeField] private float _maxHP;
    [SerializeField] private float _actualHP;
    [SerializeField] protected float _velocity;
    [SerializeField] protected float _baseDamage;


    [SerializeField] private Rigidbody2D rb;

    private bool _isDead = false;

    protected void Start()
    {
        _actualHP = _maxHP;

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    public void TakeDamage(float quantity)
    {
        if (_isDead || quantity <= 0)
        {
            return;
        }

        _actualHP -= quantity;

        if (_actualHP <= 0)
        {
            _actualHP = 0;
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
        Destroy(gameObject);
    }
}
