using Unity.VisualScripting;
using UnityEngine;

public class PlayerGrenade : PlayerBullet
{
    [Header("Grenade")]
    [SerializeField] private float _detonationTime = 2f;
    [SerializeField] private int _projQuantity;
    [SerializeField] private PlayerShootingType _projectile;

    void Awake()
    {
        Invoke(nameof(Explode), _detonationTime);
    }

    private void Explode()
    {
        for(int i = 0; i < _projQuantity; i++)
        {
            Shoot();
        }

        Destroy(gameObject);
    }

    private void Shoot()
    {
        Vector2 dir = Random.onUnitCircle;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        PlayerBullet bullet = Instantiate(_projectile.bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<PlayerBullet>();
        bullet.Init(_projectile.damage, _projectile.speed, 0.15f);
    }
}
