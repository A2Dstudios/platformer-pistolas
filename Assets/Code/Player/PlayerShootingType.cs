using UnityEngine;

[CreateAssetMenu(menuName = "A2D/Player Shooting Type")]
public class PlayerShootingType : ScriptableObject
{
    public GameObject bulletPrefab;
    public float damage;
    public float speed;
    public float fireRate;

    public int nBullets;
    public float bulletSpread;
}
