
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private LayerMask _obstacleLayers;
    [SerializeField] private float _lifetime = 4f;

    private float _damage;
    private bool _initialized;

    public void Init(Vector2 direction, float damage, float speed)
    {
        _damage = damage;

        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        if (_rb == null)
        {
            Debug.LogError(
                "EnemyProjectile necesita un Rigidbody2D.",
                this
            );

            Destroy(gameObject);
            return;
        }

        direction.Normalize();
        transform.right = direction;
        _rb.linearVelocity = direction * speed;

        _initialized = true;

        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_initialized)
        {
            return;
        }

        PlayerHealth player =
            other.GetComponentInParent<PlayerHealth>();

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
