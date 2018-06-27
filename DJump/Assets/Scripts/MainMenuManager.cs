using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static readonly string CurrentLevelKey = "CurrentLevel";
    public static readonly string Level1EnabledKey = "Level1Enabled";
    public static readonly string Level2EnabledKey = "Level2Enabled";
    public static readonly string Level3EnabledKey = "Level3Enabled";
    public static readonly string FreeRunKey = "FreeRun";
    public static readonly string FreeRunEnabledKey = "FreeRunEnabled";
    public static readonly string ScoreKey = "Score";

    public string LevelSelectorSceneName;

    private string _saveFilePath = @".\save.xml";
    private bool _level1Enabled = false;
    private bool _level2Enabled = false;
    private bool _level3Enabled = false;
    private bool _freeRunEnabled = false;

    private string _saveXmlTemplate =
        "<RatJump>"
      + "   <Levels freeRunEnabled=\"false\">"
      + "     <Level1 enabled=\"true\" />"
      + "     <Level2 enabled=\"false\" />"
      + "     <Level3 enabled=\"false\" />"
      + "   </Levels>"
      + "</RatJump>";

    public void Start()
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

        var levelsNode = saveXml.Elements().FirstOrDefault(n => n.Name.LocalName.Equals("Levels", StringComparison.OrdinalIgnoreCase));
        if (levelsNode != null)
        {
            _level1Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level1.ToString());
            _level2Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level2.ToString());
            _level3Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level3.ToString());
            _freeRunEnabled = CheckIfFreeRunIsEnabled(levelsNode);
        }

        PlayerPrefs.SetString(Level1EnabledKey, _level1Enabled.ToString());
        PlayerPrefs.SetString(Level2EnabledKey, _level2Enabled.ToString());
        PlayerPrefs.SetString(Level3EnabledKey, _level3Enabled.ToString());
        PlayerPrefs.SetString(FreeRunEnabledKey, _freeRunEnabled.ToString());
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(LevelSelectorSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private bool CheckIfLevelIsEnabled(XElement levelsNode, string levelName)
    {
        var levelNode = levelsNode.Elements().FirstOrDefault(e => e.Name.LocalName.Equals(levelName, StringComparison.OrdinalIgnoreCase));
        if (levelNode != null)
        {
            var enabledAttribute = levelNode.Attributes().FirstOrDefault(a => a.Name.LocalName.Equals("enabled", StringComparison.OrdinalIgnoreCase));
            if (enabledAttribute != null)
            {
                return enabledAttribute.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
            }
        }

        return false;
    }

    private bool CheckIfFreeRunIsEnabled(XElement levelsNode)
    {
        var freeRunEnabledAttribute = levelsNode.Attributes().FirstOrDefault(e => e.Name.LocalName.Equals("freeRunEnabled", StringComparison.OrdinalIgnoreCase));
        if (freeRunEnabledAttribute != null)
        {
            return freeRunEnabledAttribute.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

    public enum Levels
    {
        Level1,
        Level2,
        Level3
    }
}
