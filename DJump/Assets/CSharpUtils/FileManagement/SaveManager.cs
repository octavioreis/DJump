using System;
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

    public bool Level1Enabled { get; set; }
    public bool Level2Enabled { get; set; }
    public bool Level3Enabled { get; set; }
    public bool FreeRunEnabled { get; set; }

    private readonly string _saveFilePath = @".\save.xml";
    private readonly string _saveXmlTemplate =
        "<RatJump>"
      + "   <Levels freeRunEnabled=\"False\">"
      + "     <Level1 enabled=\"True\" />"
      + "     <Level2 enabled=\"False\" />"
      + "     <Level3 enabled=\"False\" />"
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

        var levelsNode = saveXml.Elements().FirstOrDefault(n => n.Name.LocalName.Equals("Levels", StringComparison.OrdinalIgnoreCase));
        if (levelsNode != null)
        {
            Level1Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level1.ToString());
            Level2Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level2.ToString());
            Level3Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level3.ToString());
            FreeRunEnabled = CheckIfFreeRunIsEnabled(levelsNode);
        }
    }

    public void Save()
    {
        var saveXml = XElement.Parse(
            "<RatJump>"
          + "   <Levels freeRunEnabled=\"" + FreeRunEnabled.ToString() + "\">"
          + "     <Level1 enabled=\"" + Level1Enabled.ToString() + "\" />"
          + "     <Level2 enabled=\"" + Level2Enabled.ToString() + "\" />"
          + "     <Level3 enabled=\"" + Level3Enabled.ToString() + "\" />"
          + "   </Levels>"
          + "</RatJump>");

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