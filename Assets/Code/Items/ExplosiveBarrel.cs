
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{

    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _explosionDamage = 50f;
    [SerializeField] private GameObject _explosionPrefab;

    private bool _hasExploded = false;

    public void Explode()
    {
        if (_hasExploded)
        {
            return;
        }

        _hasExploded = true;

        if (_explosionPrefab != null)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            transform.position,
            _explosionRadius
        );

        foreach (Collider2D collider in colliders)
        {
            MobBase enemy = collider.GetComponentInParent<MobBase>();

            if (enemy != null)
            {
                enemy.TakeDamage(_explosionDamage);
            }
        }

        Destroy(gameObject);
    }
}
