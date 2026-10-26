
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] private float _lifetime = 4f;
    [SerializeField] private LayerMask _obstacleLayers;
    [SerializeField] private Rigidbody2D _rb;

    private float _damage;

    public void Init(Vector2 direction, float damage, float speed, float lifetime = -1f)
    {
        _damage = damage;

        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        direction.Normalize();

        transform.right = direction;
        _rb.linearVelocity = direction * speed;

        float finalLifetime = _lifetime;

        if (lifetime > 0f)
        {
            finalLifetime = lifetime;
        }

        Destroy(gameObject, finalLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
            return;
        }

        if (((1 << other.gameObject.layer) & _obstacleLayers) != 0)
        {
            Destroy(gameObject);
        }
    }
}
