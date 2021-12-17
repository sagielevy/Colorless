using System;
using System.Collections.Generic;
using System.Text;

public class LevelGenerator
{
    private readonly Dictionary<string, string> ItemColors = new Dictionary<string, string>
    {
        { "black", "#000000" },
        { "white", "#FFFFFF" },
        { "red", "#FF0000" },
        { "green", "#00FF00" },
        { "blue", "#0000FF"},
        { "cyan", "#00FFFF" },
        { "magenta", "#FF00FF" },
        { "pink", "#FF00FF" },
        { "purple", "#FF00FF" },
        { "yellow", "#FFFF00" }
    };

    public string GenerateLevelText(List<string> selectedItemNames)
    {
        var levelInstructions = new StringBuilder();
        levelInstructions.Append("<align=\"center\"><b>Decorate living room tree with:\n\n</b></align>");

        foreach (var item in selectedItemNames)
        {
            var colorName = GetItemColor(item);

            levelInstructions.AppendFormat($"<size=80%><color={colorName}><line-height=120%>* {item}\n");
        }

        return levelInstructions.ToString();
    }

    private string GetItemColor(string item)
    {
        var itemLower = item.ToLower();

        foreach (var colorName in ItemColors)
        {
            if (itemLower.Contains(colorName.Key)) // I should use an override for the dictionary comparator...
            {
                return colorName.Value;
            }
        }

        throw new Exception($"item: {item} does not have a valid color in its name");
    }

    public List<string> GenerateLevel(List<string> itemNames, int itemCount)
    {
        var selectedItems = new List<string>();

        for (int i = 0; i < itemCount; i++)
        {
            var randIndex = UnityEngine.Random.Range(0, itemNames.Count);

            selectedItems.Add(itemNames[randIndex]);
            itemNames.RemoveAt(randIndex);
        }

        return selectedItems;
    }
}
