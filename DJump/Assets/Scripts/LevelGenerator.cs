﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static int PlatformsToBuild;

    public GameObject PlatformPrefab;
    public GameObject EnemyPrefab;
    public Transform CameraTransform;
    public float LevelWidth = 3f;

    private Vector3 _currentPlatformPosition;
    private List<EnemySpawnInfo> _enemySpawnSettings;
    private List<PlatformSpawnInfo> _platformSpawnSettings;

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
        _platformSpawnSettings = new List<PlatformSpawnInfo>
        {
            new PlatformSpawnInfo(15, 0.5f, 1.5f),
            new PlatformSpawnInfo(25, 0.6f, 1.9f),
            new PlatformSpawnInfo(30, 0.7f, 2.2f),
            new PlatformSpawnInfo(35, 0.8f, 2.5f),
            new PlatformSpawnInfo(40, 0.9f, 2.7f),
            new PlatformSpawnInfo(0, 1f, 3f)
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
        var currentPlatformSpawnSetting = _platformSpawnSettings.FirstOrDefault();
        if (currentPlatformSpawnSetting == null)
            return;

        _currentPlatformPosition.y += Random.Range(currentPlatformSpawnSetting.MinY, currentPlatformSpawnSetting.MaxY);
        _currentPlatformPosition.x = Random.Range(-LevelWidth, LevelWidth);

        var platformObject = Instantiate(PlatformPrefab, _currentPlatformPosition, Quaternion.identity);
        var platform = platformObject.GetComponent<Platform>();
        if (platform != null)
            platform.CameraTransform = CameraTransform;

        if (currentPlatformSpawnSetting.NumberOfSpawns <= 0)
        {
            if (_platformSpawnSettings.Count > 1)
                _platformSpawnSettings.Remove(currentPlatformSpawnSetting);
        }
        else
            currentPlatformSpawnSetting.NumberOfSpawns--;
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
            enemyPosition.x = Random.Range(-LevelWidth, LevelWidth);

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

    private class PlatformSpawnInfo
    {
        public int NumberOfSpawns { get; set; }
        public float MinY { get; set; }
        public float MaxY { get; set; }

        public PlatformSpawnInfo(int numberOfSpawns, float minY, float maxY)
        {
            NumberOfSpawns = numberOfSpawns;
            MinY = minY;
            MaxY = maxY;
        }
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