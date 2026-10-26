using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    protected float _damage;
    protected float _speed;
    protected LayerMask _obstacleLayers;

    [Header("Refs")]
    [SerializeField] private Rigidbody2D _rb;

    public virtual void Init(PlayerShootingType data)
    {
        _damage = data.damage;
        _speed = data.speed;
        _obstacleLayers = data.obstacleLayers;

        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = _speed * transform.right;

        if (data.lifetime > 0) Invoke(nameof(Consume), data.lifetime);
    }

   
private void OnTriggerEnter2D(Collider2D other)
{
    ExplosiveBarrel barrel = other.GetComponentInParent<ExplosiveBarrel>();

    if (barrel != null)
    {
        barrel.Explode();
        Consume();
        return;
    }

    if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    {
        if (!other.TryGetComponent(out MobBase enemy)) return;
        enemy.TakeDamage(_damage);
    }

    if (((1 << other.gameObject.layer) & _obstacleLayers) != 0)
    {
        Consume();
    }
}


    public virtual void Consume()
    {


        Destroy(gameObject);
    }
}
