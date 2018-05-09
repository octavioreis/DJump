using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject PlatformPrefab;
    public GameObject EnemyPrefab;
    public int NumberOfPlatforms = 200;
    public float LevelWidth = 3f;
    public float MinY = .5f;
    public float MaxY = 1.8f;

    private void Start()
    {
        var platformPosition = new Vector3();

        for (int i = 0; i < NumberOfPlatforms; i++)
        {
            platformPosition.y += Random.Range(MinY, MaxY);
            platformPosition.x = Random.Range(-LevelWidth, LevelWidth);

            Instantiate(PlatformPrefab, platformPosition, Quaternion.identity);

            if (i % 50 == 0 && i != 0)
            {
                var enemyPosition = platformPosition;
                enemyPosition.y += 1;

                Instantiate(EnemyPrefab, enemyPosition, Quaternion.identity);
            }
        }
    }
}
