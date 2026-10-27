using Unity.Cinemachine;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private float _offset = 2f;
    [SerializeField] private float _smoothTime = 0.2f;

    private CinemachinePositionComposer _composer;
    private PlayerInputs _playerInputs;
    private Vector3 _lookDir;
    private Vector3 _velocity;

    void Awake()
    {
        _composer = GetComponent<CinemachinePositionComposer>();
        _playerInputs = new PlayerInputs();
        _playerInputs.Player.Enable();
    }

    void Update()
    {
        Vector2 input = _playerInputs.Player.Shoot.ReadValue<Vector2>();

        _lookDir = Vector3.SmoothDamp(_lookDir, input, ref _velocity, _smoothTime);

        var comp = _composer.Composition;
        comp.ScreenPosition = new Vector2(-_lookDir.x, _lookDir.y) * _offset;
        _composer.Composition = comp;
    }

    private void OnDestroy()
    {
        _playerInputs.Player.Disable();
    }
}
