using UnityEngine;

public abstract class PickableItem : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float _animSpeed = 2f;
    [SerializeField] private float _animHeight= 0.3f;
    private float _startY;

    private void Awake()
    {
        _startY = transform.position.y;
    }

    private void Update() // animacion arriba y abajo
    {
        float y = _startY + _animHeight * Mathf.Abs(Mathf.Sin(Time.time * _animSpeed));
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Pickup(other);
    }

    protected abstract void Pickup(Collider2D other);
}
