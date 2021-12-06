using System;
using System.Collections.Generic;
using System.Linq;
using DevionGames.InventorySystem;
using UnityEngine;

public class GameStateManager
{
    private const int itemCount = 4;

    private List<string> levelData;
    private ItemCollection previousRoomItemCollection;

    public GameStateManager(ItemDatabase itemDatabase, FilterController filterController,
        GameWinChecker gameWinChecker, TMPro.TextMeshProUGUI instructionsText,
        ItemCollection roomItemCollection)
    {
        PlayerPrefs.DeleteAll();
        previousRoomItemCollection = roomItemCollection;

        var levelGenerator = new LevelGenerator();

        var itemNames = itemDatabase.items.Select(x => x.name).ToList();

        levelData = levelGenerator.GenerateLevel(itemNames, itemCount);

        gameWinChecker.SetLevelData(levelData.ToHashSet());
        instructionsText.text = new LevelGenerator().GenerateLevelText(levelData);

        filterController.SetColorlessFactor(0);
        filterController.SetClearColor();
    }

    public void StartLevel(FilterController filterController,
        GameWinChecker gameWinChecker, TMPro.TextMeshProUGUI instructionsText,
        ItemCollection roomItemCollection)
    {
        gameWinChecker.SetLevelData(levelData.ToHashSet());
        instructionsText.text = new LevelGenerator().GenerateLevelText(levelData);

        filterController.SetColorlessFactor(0);
        filterController.SetClearColor();

        // Migrate items to new scene and then reference new collection.
        foreach (var item in previousRoomItemCollection)
        {
            roomItemCollection.Add(item);
        }

        previousRoomItemCollection = roomItemCollection;
    }
}
