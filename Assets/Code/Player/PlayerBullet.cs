using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float _damage;
    private float _speed;

    [Header("Refs")]
    [SerializeField] private Rigidbody2D _rb;

    public void Init(float damage, float speed)
    {
        _damage = damage;
        _speed = speed;

        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = _speed * transform.right;
    }

    private void Awake()
    {
        Destroy(gameObject, 10f); // super mega provisional tio
    }

    private void OnTriggerEnter(Collider other)
    {
        // matar asesinar destruir
    }
}
