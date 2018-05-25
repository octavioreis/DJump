public class PlatformSpawnInfiniteCollection : PlatformSpawnCollection
{
    private readonly float _minY;
    private readonly float _maxY;

    public PlatformSpawnInfiniteCollection(float minY, float maxY)
    {
        _minY = minY;
        _maxY = maxY;
    }

    public override PlatformSpawn FetchNextPlatformSpawn()
    {
        return new PlatformSpawn(_minY, _maxY, false);
    }

    public override bool IsEmpty()
    {
        return false;
    }
}
