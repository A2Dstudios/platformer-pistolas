using TMPro;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private float _offset = 2f;
    [SerializeField] private float _speed = 1f;

    private Transform _playerTransform;
    private PlayerInputs _playerInputs;
    private Vector3 _lookDir;

    void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Player.Enable();

        _playerTransform = transform.parent;

        // desacoplar para evitar problemas
        transform.SetParent(null);
    }

    void Update()
    {
        Vector2 input = _playerInputs.Player.Shoot.ReadValue<Vector2>();
        bool hasInput = input.sqrMagnitude > 0;

        _lookDir = Vector3.MoveTowards(_lookDir, input, _speed * Time.deltaTime);

        transform.position = _playerTransform.position + _lookDir * _offset;
    }
}
