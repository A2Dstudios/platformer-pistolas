
using UnityEngine;

public class wizardMob : MobBase
{
    [SerializeField] private float _minTpTime = 1f;
    [SerializeField] private float _maxTpTime = 5f;
    [SerializeField] private float _minTpDistance = 3f;
    [SerializeField] private float _maxTpDistance = 7f;
    [SerializeField] private float _minAttackCooldown = 0.8f;
    [SerializeField] private float _maxAttackCooldown = 2.4f;
    [SerializeField] private float _projectileSpeed = 6f;
    private float _tpTime;
    private Vector3 _tpPos;

    [Header("Refs")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Animator _animator;

    private float _nextAttackTime;

    private new void Awake()
    {
        base.Awake();

        _tpTime = Time.time + Random.Range(_minTpTime, _maxTpTime);
        _nextAttackTime = Time.time + Random.Range(_minAttackCooldown, _maxAttackCooldown);
    }

    private void Update()
    {
        if(Time.time > _tpTime)
        {
            Move();
            _tpTime = Time.time + Random.Range(_minTpTime, _maxTpTime);
        }

        if(Time.time > _nextAttackTime)
        {
            Attack();
            _nextAttackTime = Time.time + Random.Range(_minAttackCooldown, _maxAttackCooldown);
        }

        int flip = _player.position.x > transform.position.x ? -1 : 1;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * flip, transform.localScale.y, transform.localScale.z);
    }

    public override void Move()
    {
        if (_player == null || _rb == null)
        {
            return;
        }

        Vector3 tpSpot = Vector3.zero;
        bool validSpot = false;

        while (!validSpot)
        {
            float distance = Random.Range(_minTpDistance, _maxTpDistance);
            Vector3 direction = Random.onUnitCircle;

            tpSpot = _player.position + direction * distance;

            Collider2D[] hits = Physics2D.OverlapCircleAll(tpSpot, 1f);
            bool hitGround = false;

            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    hitGround = true;
                    break;
                }
            }

            if (!hitGround) validSpot = true;
        }

        _tpPos = tpSpot;
        _animator.SetTrigger("move");
    }

    public void Teleport()
    {
        transform.position = _tpPos;
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
