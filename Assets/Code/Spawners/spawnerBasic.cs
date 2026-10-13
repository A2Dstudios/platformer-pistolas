using UnityEngine;

public class spawnerBasic : MonoBehaviour
{
    [SerializeField] private GameObject basicMob;
    [SerializeField] private Transform _player;

    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;

    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    [SerializeField] private float minDistanceFromPlayer = 5f;



    void Start()
    {
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Vector2 randomPosition;

        do
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            randomPosition = new Vector2(randomX, randomY);

        } while (
            Vector2.Distance(randomPosition, _player.position) < minDistanceFromPlayer ||
            Physics2D.OverlapCircle(randomPosition, 1.5f) != null
        );

        Instantiate(basicMob, randomPosition, Quaternion.identity);
    }
}