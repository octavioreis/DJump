using System.Collections.Generic;
using System.Linq;

public static class PlayerScoreExtensions
{
    public static IEnumerable<PlayerScore> FilterTop10Scores(this IEnumerable<PlayerScore> playerScores)
    {
        var counter = 0;
        var scores = playerScores
            .Where(s => !s.Name.IsNullOrWhiteSpace() && s.Score > 0)
            .OrderByDescending(s => s.Score);

        foreach (var score in scores)
        {
            counter++;

            yield return score;

            if (counter == 10)
                yield break;
        }
    }
}
