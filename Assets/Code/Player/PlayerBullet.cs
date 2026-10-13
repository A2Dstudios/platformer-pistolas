using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    protected float _damage;
    protected float _speed;

    [Header("Refs")]
    [SerializeField] private Rigidbody2D _rb;

    public void Init(float damage, float speed, float lifetime = 10f)
    {
        _damage = damage;
        _speed = speed;

        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = _speed * transform.right;

        Destroy(gameObject, lifetime); // super mega provisional tio
    }

    private void OnTriggerEnter(Collider other)
    {
        // matar asesinar destruir
    }
}
