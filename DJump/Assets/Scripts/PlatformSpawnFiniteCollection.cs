using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawnFiniteCollection : PlatformSpawnCollection
{
    private readonly List<PlatformSpawn> _platformSpawns = new List<PlatformSpawn>();

    public PlatformSpawnFiniteCollection(int numberOfSpawns, int numberOfMoving, float minY, float maxY)
    {
        var movingIndexes = GetRandomizedIntVector(numberOfMoving, 0, numberOfSpawns - 1);

        for (int i = 0; i < numberOfSpawns; i++)
            _platformSpawns.Add(new PlatformSpawn(minY, maxY, !movingIndexes.Contains(i)));
    }

    public override PlatformSpawn FetchNextPlatformSpawn()
    {
        var platformSpawn = _platformSpawns.FirstOrDefault();

        _platformSpawns.Remove(platformSpawn);

        return platformSpawn;
    }

    public override bool IsEmpty()
    {
        return !_platformSpawns.Any();
    }

    private int[] GetRandomizedIntVector(int vectorSize, int min, int max)
    {
        var randomInts = new int[vectorSize];

        for (int i = 0; i < vectorSize; i++)
            randomInts[i] = Random.Range(min, max);

        return randomInts;
    }
}
