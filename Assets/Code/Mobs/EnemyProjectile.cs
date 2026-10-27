
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private LayerMask _obstacleLayers;
    [SerializeField] private float _lifetime = 4f;

    [SerializeField] private float _damage;

    public void Init(Vector2 direction, float speed)
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();

        direction.Normalize();
        transform.right = direction;
        _rb.linearVelocity = direction * speed;

        _initialized = true;

        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

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
