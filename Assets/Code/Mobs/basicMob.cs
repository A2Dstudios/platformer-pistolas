
using UnityEngine;

public class basicMob : MobBase
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionRange = 6f;
    [SerializeField] private float _stopDistance = 1f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        if (_player == null || _rb == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, _player.position);

        if (distance <= _stopDistance || distance > _detectionRange)
        {
            _rb.linearVelocityX = 0f;
            return;
        }

        float direction = Mathf.Sign(
            _player.position.x - transform.position.x
        );

        _rb.linearVelocityX = direction * _velocity;
    }

    public override void Attack()
    {
        // se hace cuando haya vida del jugador jeje good
    }

    public void AssisgnPlayer(Transform transformPlayer)
    {
        _player = transformPlayer;
    }
}
