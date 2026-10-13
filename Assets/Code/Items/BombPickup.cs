using UnityEngine;

public class BombPickup : PickableItem
{
    [Header("Bomb")]
    [SerializeField] private BombType _type;

    protected override void Pickup(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerShooting player)) return;

        if (!player.TryPickupBomb(_type)) return;

        Destroy(gameObject);
    }
}
