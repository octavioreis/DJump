using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static int PlatformsToBuild;

    public GameObject PlatformPrefab;
    public GameObject EnemyPrefab;
    public Transform CameraTransform;

    private Vector3 _currentPlatformPosition;
    private List<EnemySpawnInfo> _enemySpawnSettings;
    private List<PlatformSpawnCollection> _platformSpawnSettings;

    private void Start()
    {
        InitializeEnemySpawnSettings();
        InitializePlatformSpawnSettings();

        InitializePlatforms();
    }

    private void InitializeEnemySpawnSettings()
    {
        _enemySpawnSettings = new List<EnemySpawnInfo>
        {
            new EnemySpawnInfo(3, 30),
            new EnemySpawnInfo(4, 20),
            new EnemySpawnInfo(0, 10)
        };
    }

    private void InitializePlatformSpawnSettings()
    {
        _platformSpawnSettings = new List<PlatformSpawnCollection>
        {
            new PlatformSpawnCollection(15, 0, 0.5f, 1.5f),
            new PlatformSpawnCollection(25, 3, 0.65f, 1.9f),
            new PlatformSpawnCollection(30, 6, 0.8f, 2.2f),
            new PlatformSpawnCollection(35, 12, 0.95f, 2.5f),
            new PlatformSpawnCollection(40, 24, 1.1f, 2.7f),
            new PlatformSpawnCollection(0, 0, 1.25f, 3f)
        };
    }

    private void InitializePlatforms()
    {
        _currentPlatformPosition = new Vector3
        {
            y = -1
        };

        for (int i = 0; i < 15; i++)
            SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        var currentPlatformSpawnCollection = _platformSpawnSettings.FirstOrDefault();
        if (currentPlatformSpawnCollection == null)
            return;

        var currentSpawnInfo = currentPlatformSpawnCollection.FetchNextPlatformSpawn();
        if (currentSpawnInfo == null)
            return;

        _currentPlatformPosition.y += Random.Range(currentSpawnInfo.MinY, currentSpawnInfo.MaxY);
        _currentPlatformPosition.x = Random.Range(GameManager.HorizontalMinLimit, GameManager.HorizontalMaxLimit);

        var platformObject = Instantiate(PlatformPrefab, _currentPlatformPosition, Quaternion.identity);
        var platform = platformObject.GetComponent<Platform>();
        if (platform != null)
        {
            platform.CameraTransform = CameraTransform;
            platform.Stationary = currentSpawnInfo.Stationary;
        }

        if (currentPlatformSpawnCollection.IsEmpty() && _platformSpawnSettings.Count > 1)
            _platformSpawnSettings.Remove(currentPlatformSpawnCollection);
    }

    private void SpawnEnemy()
    {
        var currentEnemySpawnSetting = _enemySpawnSettings.FirstOrDefault();
        if (currentEnemySpawnSetting == null)
            return;

        if (currentEnemySpawnSetting.PlatformsBetweenSpawn > 0)
        {
            currentEnemySpawnSetting.PlatformsBetweenSpawn--;
        }
        else
        {
            var enemyPosition = _currentPlatformPosition;
            enemyPosition.y += 1;
            enemyPosition.x = Random.Range(GameManager.HorizontalMinLimit, GameManager.HorizontalMaxLimit);

            Instantiate(EnemyPrefab, enemyPosition, Quaternion.identity);

            currentEnemySpawnSetting.ResetPlatformSpacingCounter();

            if (currentEnemySpawnSetting.NumberOfSpawns <= 0)
            {
                if (_enemySpawnSettings.Count > 1)
                    _enemySpawnSettings.Remove(currentEnemySpawnSetting);
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
        public int NumberOfSpawns { get; set; }
        public int PlatformsBetweenSpawn { get; set; }

        private readonly int _platformSpacingBackup;

        public EnemySpawnInfo(int numberOfSpawns, int platformsBetweenSpawn)
        {
            PlatformsBetweenSpawn = platformsBetweenSpawn;
            NumberOfSpawns = numberOfSpawns;

            _platformSpacingBackup = platformsBetweenSpawn;
        }

        public void ResetPlatformSpacingCounter()
        {
            PlatformsBetweenSpawn = _platformSpacingBackup;
        }
    }
}