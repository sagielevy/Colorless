using System;
using System.Collections.Generic;
using System.Linq;
using DevionGames.InventorySystem;
using UnityEngine;

public class GameStateManager
{
    public const int Room1Index = 0;
    public const int Room2Index = 1;
    public const int RoomFinalIndex = 2;

    public const int ItemGoalCount = 4;

    private List<string> levelData;
    //private ItemCollection previousRoomItemCollection;

    public GameStateManager(ItemDatabase itemDatabase, FilterController filterController,
        GameWinChecker gameWinChecker, TMPro.TextMeshProUGUI instructionsText)
    {
        PlayerPrefs.DeleteAll();
        //previousRoomItemCollection = roomItemCollection;

        var levelGenerator = new LevelGenerator();

        var itemNames = itemDatabase.items.Select(x => x.name).ToList();

        levelData = levelGenerator.GenerateLevel(itemNames, ItemGoalCount);

        EnterRoom(filterController, gameWinChecker, instructionsText);
    }

    public void EnterRoom(FilterController filterController,
        GameWinChecker gameWinChecker, TMPro.TextMeshProUGUI instructionsText)
    {
        gameWinChecker.SetLevelData(levelData.ToHashSet());
        instructionsText.text = new LevelGenerator().GenerateLevelText(levelData);

        filterController.SetColorlessFactor(0);
        filterController.SetClearColor();
    }

    //public void StartLevel(FilterController filterController,
    //    GameWinChecker gameWinChecker, TMPro.TextMeshProUGUI instructionsText)
    //{
    //    gameWinChecker.SetLevelData(levelData.ToHashSet());
    //    instructionsText.text = new LevelGenerator().GenerateLevelText(levelData);

    //    filterController.SetColorlessFactor(0);
    //    filterController.SetClearColor();

        // Migrate items to new scene and then reference new collection.
        //foreach (var item in previousRoomItemCollection)
        //{
        //    roomItemCollection.Add(item);
        //}

        //previousRoomItemCollection = roomItemCollection;
    //}
}
