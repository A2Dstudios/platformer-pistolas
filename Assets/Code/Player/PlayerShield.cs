
using UnityEngine;

public class PlayerShield : MonoBehaviour
{

    [SerializeField] private float _duration = 5f;

    private bool _hasShield;
    private float _shieldEndTime;

    public bool HasShield()
    {
        return _hasShield;
    }

    public void ActivateShield()
    {
        _hasShield = true;
        _shieldEndTime = Time.time + _duration;
    }

    private void Update()
    {
        if (_hasShield && Time.time >= _shieldEndTime)
        {
            _hasShield = false;
        }
    }
}
