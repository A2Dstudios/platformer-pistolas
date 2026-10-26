using UnityEngine;

public abstract class MobBase : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected float _maxHP;
    [SerializeField] protected float _velocity;
    [SerializeField] protected float _baseDamage;
    protected float _hP;


    [SerializeField] protected Rigidbody2D _rb;

    private bool _isDead = false;

    protected void Awake()
    {
        _hP = _maxHP;

        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
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
        Destroy(gameObject);
    }
}
