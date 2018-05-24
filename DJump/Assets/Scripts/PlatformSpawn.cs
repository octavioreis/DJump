public class PlatformSpawn
{
    public readonly float MinY;
    public readonly float MaxY;
    public readonly bool Stationary;

    public PlatformSpawn(float minY, float maxY, bool stationary = true)
    {
        MinY = minY;
        MaxY = maxY;
        Stationary = stationary;
    }
}
