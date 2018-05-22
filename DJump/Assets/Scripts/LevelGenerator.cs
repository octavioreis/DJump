using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static int PlatformsToBuild;

    public GameObject PlatformPrefab;
    public GameObject EnemyPrefab;
    public Transform CameraTransform;
    public float LevelWidth = 3f;
    public float MinY = .5f;
    public float MaxY = 1.8f;

    private Vector3 currentPlatformPosition;

    private void Start()
    {
        currentPlatformPosition = new Vector3();

        InitializePlatforms();
    }

    private void InitializePlatforms()
    {
        for (int i = 0; i < 15; i++)
        {
            BuildPlatform();

            //if (i % 50 == 0 && i != 0)
            //{
            //    var enemyPosition = currentPlatformPosition;
            //    enemyPosition.y += 1;

            //    Instantiate(EnemyPrefab, enemyPosition, Quaternion.identity);
            //}
        }
    }

    private void BuildPlatform()
    {
        currentPlatformPosition.y += Random.Range(MinY, MaxY);
        currentPlatformPosition.x = Random.Range(-LevelWidth, LevelWidth);

        var platformObject = Instantiate(PlatformPrefab, currentPlatformPosition, Quaternion.identity);
        var platform = platformObject.GetComponent<Platform>();
        if (platform != null)
            platform.CameraTransform = CameraTransform;
    }

    private void Update()
    {
        for (int i = 0; i < PlatformsToBuild; i++)
            BuildPlatform();

        PlatformsToBuild = 0;
    }
}
