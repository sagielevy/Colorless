using System;
using System.Collections.Generic;
using System.Text;

public class LevelGenerator
{
    public string GenerateLevelText(List<string> selectedItemNames)
    {
        var levelInstructions = new StringBuilder();
        levelInstructions.Append("Objects to find:\n\n");

        foreach (var item in selectedItemNames)
        {
            levelInstructions.AppendFormat($"{item}.\n");
        }

        return levelInstructions.ToString();
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
