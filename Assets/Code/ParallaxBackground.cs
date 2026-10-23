using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float _parallaxEffect;
    [SerializeField] private int _tileCount = 6;

    private float _startPos, _length, _span;
    private float _startY, _startCamY;
    private Transform _cam;

    void Awake()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
        _cam = Camera.main.transform;
        _span = _length * _tileCount;
        _startCamY = _cam.position.y;
        _startY = transform.position.y;
    }

    void LateUpdate()
    {
        float camX = _cam.position.x;

        float relative = _startPos + camX * (_parallaxEffect - 1f);

        float half = _span * 0.5f;
        float wrapped = Mathf.Repeat(relative + half, _span) - half;

        float y = _startY + (_cam.position.y - _startCamY) * _parallaxEffect;
        transform.position = new Vector3(camX + wrapped, y, transform.position.z);
    }
}
