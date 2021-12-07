using UnityEngine;
using System.Collections;
using DevionGames;
using DevionGames.InventorySystem;
using System.Collections.Generic;
using System.Linq;

public class GameWinChecker : MonoBehaviour
{
    [SerializeField] private ItemCollection finalRoomItems;
    [SerializeField] private GameObject winDisplay;
    [SerializeField] private GameInstructionController gameInstructionController;
    [SerializeField] private FilterController filterController;
    [SerializeField] private Canvas RoomCanvas;
    [SerializeField] private float animateWinSpeed = 0.4f;

    private HashSet<string> levelData;
    private bool hasWon = false;
    private float wonTime;

    public void SetLevelData(HashSet<string> levelData)
    {
        this.levelData = levelData;
    }

    public void UpdateLevel(ItemCollection finalRoomItems,
        GameObject winDisplay, FilterController filterController)
    {
        this.finalRoomItems = finalRoomItems;
        this.winDisplay = winDisplay;
        this.filterController = filterController;
    }

    public void CheckForWin()
    {
        var userItems = finalRoomItems.Select(x => x.Name).ToHashSet();

        if (userItems.SetEquals(levelData))
        {
            hasWon = true;
            wonTime = Time.time;
            winDisplay.SetActive(true);
        }
    }

    private void Update()
    {
        if (!hasWon) return;

        RoomCanvas.enabled = false;
        gameInstructionController.TurnOffInstructions();
        var factor = Mathf.Lerp(0, 1, (Time.time - wonTime) * animateWinSpeed);
        filterController.SetClearColor();
        filterController.SetColorlessFactor(factor);
    }
}
