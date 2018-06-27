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

    public bool Level2Enabled { get; set; }
    public bool Level3Enabled { get; set; }
    public bool FreeRunEnabled { get; set; }
    public readonly List<PlayerScore> PlayerScores = new List<PlayerScore>();

    private readonly string _saveFilePath = @".\save.xml";
    private readonly string _saveXmlTemplate =
        "<RatJump>"
      + "   <Levels FreeRunEnabled=\"False\">"
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
            FreeRunEnabled = CheckIfFreeRunIsEnabled(levelsNode);
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
        levelsNode.Add(new XAttribute(Consts.FreeRunEnabled, FreeRunEnabled));
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
            var enabledAttribute = levelNode.Attributes().FirstOrDefault(a => a.Name.LocalName.Equals("enabled", StringComparison.OrdinalIgnoreCase));
            if (enabledAttribute != null)
                return enabledAttribute.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

    private bool CheckIfFreeRunIsEnabled(XElement levelsNode)
    {
        var freeRunEnabledAttribute = levelsNode.Attributes().FirstOrDefault(e => e.Name.LocalName.Equals("freeRunEnabled", StringComparison.OrdinalIgnoreCase));
        if (freeRunEnabledAttribute != null)
            return freeRunEnabledAttribute.Value.Equals("true", StringComparison.OrdinalIgnoreCase);

        return false;
    }
}