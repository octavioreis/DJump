using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public class SaveManager
{
    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            return _instance ?? (_instance = new SaveManager());
        }
    }

    public readonly List<PlayerScore> PlayerScores = new List<PlayerScore>();
    public bool Level2Enabled { get; set; }
    public bool Level3Enabled { get; set; }
    public bool StoryModeCompleted { get; set; }
    public bool TutorialCompleted { get; set; }

    private readonly string _saveFilePath = @".\save.xml";
    private readonly string _saveXmlTemplate =
        "<RatJump>"
      + "   <Levels StoryModeCompleted=\"False\" TutorialCompleted=\"False\">"
      + "     <Level2 Enabled=\"False\" />"
      + "     <Level3 Enabled=\"False\" />"
      + "   </Levels>"
      + "</RatJump>";

    private SaveManager()
    {
    }

    public void Load()
    {
        XElement saveXml;

        if (File.Exists(_saveFilePath))
        {
            saveXml = XElement.Load(_saveFilePath);
        }
        else
        {
            saveXml = XElement.Parse(_saveXmlTemplate);
            saveXml.Save(_saveFilePath);
        }

        var levelsNode = saveXml.Elements().GetElement(Consts.Levels);
        if (levelsNode != null)
        {
            Level2Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level2.ToString());
            Level3Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level3.ToString());
            StoryModeCompleted = CheckIfStoryModeIsCompleted(levelsNode);
            TutorialCompleted = CheckIfTutorialIsCompleted(levelsNode);
        }

        var scoresNode = saveXml.Elements().GetElement(Consts.HighScores);
        if (scoresNode != null)
        {
            string playerName;
            string playerScoreString;
            int playerScoreInt;
            var scores = new List<PlayerScore>();

            foreach (var scoreNode in scoresNode.Elements().GetElements(Consts.Score))
            {
                playerName = scoreNode.Attributes().GetAttributeValue(Consts.PlayerName);
                playerScoreString = scoreNode.Attributes().GetAttributeValue(Consts.Value);

                if (!playerName.IsNullOrWhiteSpace() && int.TryParse(playerScoreString, out playerScoreInt))
                    scores.Add(new PlayerScore(playerName, playerScoreInt));
            }

            PlayerScores.Clear();
            PlayerScores.AddRange(scores.FilterTop10Scores());
        }
    }

    public void Save()
    {
        var saveXml = new XElement(Consts.RatJump);
        var levelsNode = new XElement(Consts.Levels);
        var level2Node = new XElement(Levels.Level2.ToString());
        var level3Node = new XElement(Levels.Level3.ToString());

        level2Node.Add(new XAttribute(Consts.Enabled, Level2Enabled));
        level3Node.Add(new XAttribute(Consts.Enabled, Level3Enabled));
        levelsNode.Add(new XAttribute(Consts.StoryModeCompleted, StoryModeCompleted));
        levelsNode.Add(new XAttribute(Consts.TutorialCompleted, TutorialCompleted));
        levelsNode.Add(level2Node);
        levelsNode.Add(level3Node);
        saveXml.Add(levelsNode);

        if (PlayerScores.Any())
        {
            var highScoresNode = new XElement(Consts.HighScores);

            foreach (var playerScore in PlayerScores.FilterTop10Scores())
            {
                var scoreNode = new XElement(Consts.Score);
                scoreNode.Add(new XAttribute(Consts.PlayerName, playerScore.Name));
                scoreNode.Add(new XAttribute(Consts.Value, playerScore.Score));

                highScoresNode.Add(scoreNode);
            }

            if (highScoresNode.Elements().Any())
                saveXml.Add(highScoresNode);
        }

        saveXml.Save(_saveFilePath);
    }

    private bool CheckIfLevelIsEnabled(XElement levelsNode, string levelName)
    {
        var levelNode = levelsNode.Elements().FirstOrDefault(e => e.Name.LocalName.Equals(levelName, StringComparison.OrdinalIgnoreCase));
        if (levelNode != null)
        {
            var enabled = levelNode.Attributes().GetAttributeValue(Consts.Enabled);
            return string.Equals(enabled, "true", StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

    private bool CheckIfStoryModeIsCompleted(XElement levelsNode)
    {
        var storyModeCompleted = levelsNode.Attributes().GetAttributeValue(Consts.StoryModeCompleted);
        return string.Equals(storyModeCompleted, "true", StringComparison.OrdinalIgnoreCase);
    }

    private bool CheckIfTutorialIsCompleted(XElement levelsNode)
    {
        var tutorialCompleted = levelsNode.Attributes().GetAttributeValue(Consts.TutorialCompleted);
        return string.Equals(tutorialCompleted, "true", StringComparison.OrdinalIgnoreCase);
    }
}