using System;
using System.Collections.Generic;
using System.Text;

public class LevelGenerator
{
    private struct ItemColorData
    {
        public string color;
        public string? highlight;
    }

    private readonly Dictionary<string, ItemColorData> ItemColors = new Dictionary<string, ItemColorData>
    {
        { "black", new ItemColorData { color="#000000", highlight="#FFFFFF30"} },
        { "white", new ItemColorData { color="#FFFFFF" } },
        { "red", new ItemColorData { color="#FF0000" } },
        { "green", new ItemColorData { color="#00FF00" } },
        { "blue", new ItemColorData { color="#0000FF", highlight="#FFFFFF30"} },
        { "cyan", new ItemColorData { color="#00FFFF" } },
        { "magenta", new ItemColorData { color="#FF00FF" } },
        { "pink", new ItemColorData { color="#FF00FF" } },
        { "purple", new ItemColorData { color="#FF00FF" } },
        { "yellow", new ItemColorData { color="#FFFF00" } }
    };

    public string GenerateLevelText(List<string> selectedItemNames)
    {
        var levelInstructions = new StringBuilder();
        levelInstructions.Append("<align=\"center\"><b>Decorate living room tree with:\n\n</b></align>");

        foreach (var item in selectedItemNames)
        {
            var itemColorData = GetItemColorData(item);
            var color = itemColorData.color;
            var itemHighlightString = $"<mark={itemColorData.highlight ?? "#00000000"}>";

            levelInstructions.AppendFormat($"<size=80%><color={color}><line-height=120%>{itemHighlightString}* {item}\n");
        }

        return levelInstructions.ToString();
    }

    private ItemColorData GetItemColorData(string item)
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
