using UnityEngine;
using DevionGames.InventorySystem;
using System.Collections.Generic;
using System.Linq;

public class GameWinChecker : MonoBehaviour
{
    [SerializeField] private ItemCollection finalRoomItems;
    [SerializeField] private GameObject winDisplay;
    [SerializeField] private TextDisplayController failDisplay;
    [SerializeField] private GameInstructionController gameInstructionController;
    [SerializeField] private FilterController filterController;
    [SerializeField] private LevelControllersManager levelControllersManager;
    [SerializeField] private Canvas RoomCanvas;
    [SerializeField] private float animateWinDuration = 3f;
    [SerializeField] private float failMessageDuration = 3f;

    private HashSet<string> levelData;

    public void SetLevelData(HashSet<string> levelData)
    {
        this.levelData = levelData;
    }

    public void CheckForWin()
    {
        var level = levelControllersManager.GetCurrentLevelController();
        var userItems = finalRoomItems.Select(x => x.Name).ToHashSet();

        if (userItems.SetEquals(levelData))
        {
            HandleWin();
        }
        else if (userItems.Count == level.LevelItemGoalCount())
        {
            failDisplay.SetDefaultOpacity(false);
            failDisplay.FadeOut(failMessageDuration);
            gameInstructionController.TurnOffInstructions();
        }
    }

    private void HandleWin()
    {
        winDisplay.SetActive(true);
        RoomCanvas.enabled = false;
        gameInstructionController.TurnOffInstructions();
        filterController.FadeInColors(animateWinDuration);
        filterController.SetClearColor();
        levelControllersManager.SetNextLevel();
    }
}
