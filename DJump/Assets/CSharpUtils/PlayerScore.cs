public class PlayerScore
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public PlayerScore(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
