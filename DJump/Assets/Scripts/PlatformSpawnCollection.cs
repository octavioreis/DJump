using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawnCollection
{
    private readonly List<PlatformSpawn> _platformSpawns = new List<PlatformSpawn>();

    public PlatformSpawnCollection(int numberOfSpawns, float minY, float maxY)
    {
        for (int i = 0; i < numberOfSpawns; i++)
            _platformSpawns.Add(new PlatformSpawn(minY, maxY));
    }

    public PlatformSpawn FetchNextPlatformSpawn()
    {
        var platformSpawn = _platformSpawns.FirstOrDefault();

        _platformSpawns.Remove(platformSpawn);

        return platformSpawn;
    }

    public bool IsEmpty()
    {
        return !_platformSpawns.Any();
    }
}
