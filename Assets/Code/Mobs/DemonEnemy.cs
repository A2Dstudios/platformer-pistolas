
using UnityEngine;

public class DemonEnemy : MobBase
{
    [Header("Demon")]
    [SerializeField] private float _acceleration = 8f;
    [SerializeField] private float _preferedDistance = 4f;
    [SerializeField] private float _minAttackCooldown = 0.8f;
    [SerializeField] private float _maxAttackCooldown = 3f;
    [SerializeField] private float _projectileSpeed = 6f;


    [Header("Refs")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Animator _animator;

    private float _nextAttackTime;

    private new void Awake()
    {
        base.Awake();

        _nextAttackTime = Time.time + Random.Range(_minAttackCooldown, _maxAttackCooldown);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            Attack();
            _nextAttackTime = Time.time + Random.Range(_minAttackCooldown, _maxAttackCooldown);
        }


        // girar
        int flip = _player.position.x > transform.position.x ? -1 : 1;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * flip, transform.localScale.y, transform.localScale.z);

        // multiplicador de la velocidad de animacion para aletear mas rapido si va mas rapido
        float curVel = _rb.linearVelocity.magnitude;
        float speedRatio = Mathf.InverseLerp(0f, _velocity, curVel);
        float speedMult = Mathf.Lerp(0.3f, 1.5f, speedRatio);
        _animator.SetFloat("speedMult", speedMult);
    }

    public override void Move()
    {
        Vector2 dirFromPlayer = ((Vector2)transform.position - (Vector2)_player.position).normalized;

        // offset hacia arriba si el demonio esta por debajo del player
        Vector2 offset = _player.position.y > transform.position.y ? Vector2.up * 0.5f : Vector2.zero;
        dirFromPlayer = (dirFromPlayer + offset).normalized;

        Vector2 targetPos = (Vector2)_player.position + (dirFromPlayer * GetTargetDistance());
        Vector2 displacementToTarget = targetPos - (Vector2)transform.position;

        Vector2 direction = displacementToTarget.normalized;
        Vector2 curVel = _rb.linearVelocity;
        Vector2 targetVel = direction * _velocity;
        Vector2 newVel = Vector2.MoveTowards(curVel, targetVel, _acceleration * Time.fixedDeltaTime);

        _rb.linearVelocity = newVel;
    }

    // ir variando la distancia alante y atras para que el movimiento parezca mas natural
    private float GetTargetDistance()
    {
        float amplitude = _preferedDistance * 0.5f;
        float frequency = Mathf.PI * 2f / 12f;

        return _preferedDistance + Mathf.Sin(Time.time * frequency) * amplitude;
    }

    protected override void Attack()
    {
        if (_player == null) return;

        _animator.SetTrigger("attack");
    }

    public void ShootOrb()
    {
        Vector2 direction = _player.position - _firePoint.position;
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        projectile.GetComponent<EnemyProjectile>().Init(direction, _projectileSpeed);
    }
}
