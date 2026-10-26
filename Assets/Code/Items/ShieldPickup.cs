
using UnityEngine;

public class ShieldPickup : PickableItem
{
    protected override void Pickup(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerShield shield)) return;

        shield.ActivateShield();

        Destroy(gameObject);
    }
}
