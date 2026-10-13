using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerShootingType _shootingType;

    [Header("Bombs")]
    [SerializeField] private float _bombThrowSpeed;
    [SerializeField] private List<BombEntry> _bombs = new();

    [Header("Refs")]
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private Transform _shootingPoint;
    private PlayerInputs _inputs;

    private float _lastShotTime;
    private Vector2 _shootingDir;
    private bool _hasInput;

    private BombType _equippedBomb;
    private bool _hasBomb;


    void Awake()
    {
        _inputs = new PlayerInputs();
    }
    private void OnEnable()
    {
        _inputs.Player.Enable();
    }
    private void OnDisable()
    {
        _inputs.Player.Disable();
    }

    void Update()
    {
        _shootingDir = _inputs.Player.Shoot.ReadValue<Vector2>();
        _hasInput = _shootingDir.magnitude > 0;

        RotateGun();
        HandleShooting();
        HandleBomb();
    }

    private void HandleBomb()
    {
        if(_inputs.Player.Bomb.IsPressed() && _hasInput && _hasBomb)
        {
            ThrowBomb(_shootingDir);
            _hasBomb = false;
        }
    }

    private void HandleShooting()
    {
        bool canShoot = Time.time > _lastShotTime + _shootingType.fireRate;

        if (_hasInput && canShoot)
        {
            Shoot(_shootingDir);
        }
    }

    private void Shoot(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        PlayerBullet bullet = Instantiate(_shootingType.bulletPrefab, _shootingPoint.position, Quaternion.Euler(0, 0, angle)).GetComponent<PlayerBullet>();
        bullet.Init(_shootingType.damage, _shootingType.speed);

        _lastShotTime = Time.time;
    }

    private void ThrowBomb(Vector2 dir)
    {
        GameObject prefab = GetBombPrefab(_equippedBomb);

        if (prefab == null) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        PlayerGrenade bomb = Instantiate(prefab, _shootingPoint.position, Quaternion.Euler(0, 0, angle)).GetComponent<PlayerGrenade>();
        bomb.Init(0f, _bombThrowSpeed);
    }

    private GameObject GetBombPrefab(BombType type) // pillar bomba asignada a ese tipo
    {
        BombEntry entry = _bombs.Find(b => b.type == type);
        return entry?.prefab;
    }


    private void RotateGun()
    {
        if(!_hasInput) return;

        float angle = Mathf.Atan2(_shootingDir.y, _shootingDir.x) * Mathf.Rad2Deg;
        _gunTransform.eulerAngles = new Vector3(0f, 0f, angle);

        // girar si apuntas a la izquierda
        int flipY = Mathf.Abs(angle) > 90f ? -1 : 1;
        _gunTransform.localScale = new Vector2(_gunTransform.localScale.x, Mathf.Abs(_gunTransform.localScale.y) * flipY);
    }


    public bool TryPickupBomb(BombType type)
    {
        if (_hasBomb) return false;

        _hasBomb = true;
        _equippedBomb = type;
        return true;
    }
}
