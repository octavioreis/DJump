using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static int PlatformsToBuild;

    public GameObject PlatformPrefab;
    public GameObject EnemyPrefab;
    public Transform CameraTransform;
    public float LevelWidth = 3f;
    public float MinY = 1f;
    public float MaxY = 3f;

    private Vector3 currentPlatformPosition;
    private List<EnemySpawnInfo> enemySpawnSettings;

    private void Start()
    {
        InitializeEnemySpawnSettings();
        InitializePlatforms();
    }

    private void InitializeEnemySpawnSettings()
    {
        enemySpawnSettings = new List<EnemySpawnInfo>
        {
            new EnemySpawnInfo(30, 3),
            new EnemySpawnInfo(20, 4),
            new EnemySpawnInfo(10, 0)
        };
    }

    private void InitializePlatforms()
    {
        currentPlatformPosition = new Vector3();
        currentPlatformPosition.y = -1;

        for (int i = 0; i < 15; i++)
            SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        currentPlatformPosition.y += Random.Range(MinY, MaxY);
        currentPlatformPosition.x = Random.Range(-LevelWidth, LevelWidth);

        var platformObject = Instantiate(PlatformPrefab, currentPlatformPosition, Quaternion.identity);
        var platform = platformObject.GetComponent<Platform>();
        if (platform != null)
            platform.CameraTransform = CameraTransform;
    }

    private void SpawnEnemy()
    {
        var currentEnemySpawnSetting = enemySpawnSettings.FirstOrDefault();
        if (currentEnemySpawnSetting == null)
            return;

        if (currentEnemySpawnSetting.PlatformSpacing > 0)
        {
            currentEnemySpawnSetting.PlatformSpacing--;
        }
        else
        {
            var enemyPosition = currentPlatformPosition;
            enemyPosition.y += 1;
            enemyPosition.x = Random.Range(-LevelWidth, LevelWidth);

            Instantiate(EnemyPrefab, enemyPosition, Quaternion.identity);

            currentEnemySpawnSetting.ResetPlatformSpacing();

            if (currentEnemySpawnSetting.NumberOfSpawns <= 0)
            {
                if (enemySpawnSettings.Count > 1)
                    enemySpawnSettings.Remove(currentEnemySpawnSetting);
            }
            else
                currentEnemySpawnSetting.NumberOfSpawns--;
        }
    }

    private void Update()
    {
        for (int i = 0; i < PlatformsToBuild; i++)
        {
            SpawnPlatform();
            SpawnEnemy();
        }

        PlatformsToBuild = 0;
    }

    private class EnemySpawnInfo
    {
        public int PlatformSpacing { get; set; }
        public int NumberOfSpawns { get; set; }

        private int platformSpacingBackup;

        public EnemySpawnInfo(int platformSpacing, int numberOfSpawns)
        {
            PlatformSpacing = platformSpacing;
            NumberOfSpawns = numberOfSpawns;

            platformSpacingBackup = platformSpacing;
        }

        public void ResetPlatformSpacing()
        {
            PlatformSpacing = platformSpacingBackup;
        }
    }
}