using UnityEngine;
using UnityEngine.Windows;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerShootingType _shootingType;

    [Header("Refs")]
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private Transform _shootingPoint;
    private PlayerInputs _inputs;

    private float _lastShotTime;
    private Vector2 _shootingDir;
    private bool _hasInput;

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


    private void RotateGun()
    {
        if(!_hasInput) return;

        float angle = Mathf.Atan2(_shootingDir.y, _shootingDir.x) * Mathf.Rad2Deg;
        _gunTransform.eulerAngles = new Vector3(0f, 0f, angle);

        // girar si apuntas a la izquierda
        int flipY = Mathf.Abs(angle) > 90f ? -1 : 1;
        _gunTransform.localScale = new Vector2(_gunTransform.localScale.x, Mathf.Abs(_gunTransform.localScale.y) * flipY);
    }
}
