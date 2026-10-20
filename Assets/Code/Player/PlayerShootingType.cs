using UnityEngine;

[CreateAssetMenu(menuName = "A2D/Player Shooting Type")]
public class PlayerShootingType : ScriptableObject
{
    public Sprite sprite;
    public GameObject bulletPrefab;
    public float damage;
    public float speed;
    public float fireRate;
    public float recoil;

    [Header("Multishot")]
    public int nBullets;
    public float bulletSpread;

    [Header("Despawning")]
    public float lifetime;
    public LayerMask obstacleLayers;

    public PlayerShootingType(float speed)
    {
        this.speed = speed;
    }
}
