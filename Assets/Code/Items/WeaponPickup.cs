using UnityEngine;

public class WeaponPickup : PickableItem
{
    [Header("Weapon")]
    [SerializeField] private PlayerShootingType _weapon;

    protected override void Pickup(Collider2D other)
    {
        if (!other.TryGetComponent(out PlayerShooting player)) return;

        player.ChanseShootingType(_weapon);

        Destroy(gameObject);
    }
}
