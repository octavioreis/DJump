using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public static class SaveManager
{
    private static readonly string _saveFilePath = @".\save.xml";
    private static readonly string _saveXmlTemplate =
        "<RatJump>"
      + "   <Levels freeRunEnabled=\"false\">"
      + "     <Level1 enabled=\"true\" />"
      + "     <Level2 enabled=\"false\" />"
      + "     <Level3 enabled=\"false\" />"
      + "   </Levels>"
      + "</RatJump>";

    public static void LoadSave(
        out bool level1Enabled,
        out bool level2Enabled,
        out bool level3Enabled,
        out bool freeRunEnabled)
    {
        XElement saveXml;
        level1Enabled = false;
        level2Enabled = false;
        level3Enabled = false;
        freeRunEnabled = false;

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
            level1Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level1.ToString());
            level2Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level2.ToString());
            level3Enabled = CheckIfLevelIsEnabled(levelsNode, Levels.Level3.ToString());
            freeRunEnabled = CheckIfFreeRunIsEnabled(levelsNode);
        }
    }

    private static bool CheckIfLevelIsEnabled(XElement levelsNode, string levelName)
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

    private static bool CheckIfFreeRunIsEnabled(XElement levelsNode)
    {
        var freeRunEnabledAttribute = levelsNode.Attributes().FirstOrDefault(e => e.Name.LocalName.Equals("freeRunEnabled", StringComparison.OrdinalIgnoreCase));
        if (freeRunEnabledAttribute != null)
        {
            return freeRunEnabledAttribute.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }
}