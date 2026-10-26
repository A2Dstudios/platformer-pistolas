
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;

    void Start()
    {
        Destroy(gameObject, _duration);
    }
}
