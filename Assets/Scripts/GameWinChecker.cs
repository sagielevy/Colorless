using UnityEngine;
using DevionGames.InventorySystem;
using System.Collections.Generic;
using System.Linq;

public class GameWinChecker : MonoBehaviour
{
    [SerializeField] private ItemCollection finalRoomItems;
    [SerializeField] private GameObject winDisplay;
    [SerializeField] private FailDisplayController failDisplay;
    [SerializeField] private GameInstructionController gameInstructionController;
    [SerializeField] private FilterController filterController;
    [SerializeField] private Canvas RoomCanvas;
    [SerializeField] private float animateWinSpeed = 0.4f;
    [SerializeField] private float failMessageDuration = 2f;

    private HashSet<string> levelData;
    private bool hasWon = false;
    private float wonTime;

    private bool hasFailed = false;
    private float lastFailTime;

    public void SetLevelData(HashSet<string> levelData)
    {
        this.levelData = levelData;
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
        else if (userItems.Count == GameManager.ItemGoalCount)
        {
            hasFailed = true;
            lastFailTime = Time.time;
            failDisplay.gameObject.SetActive(true);
            failDisplay.DefaultOpacity();
            gameInstructionController.TurnOffInstructions();
        }
    }

    private void Update()
    {
        if (hasWon)
        {
            HandleWin();
        } else if (hasFailed)
        {
            HandleFail();
        }
    }

    private void HandleWin()
    {
        RoomCanvas.enabled = false;
        gameInstructionController.TurnOffInstructions();
        var factor = Mathf.Lerp(0, 1, (Time.time - wonTime) * animateWinSpeed);
        filterController.SetClearColor();
        filterController.SetColorlessFactor(factor);
    }

    private void HandleFail()
    {
        var failTimeDelta = Time.time - lastFailTime;

        failDisplay.SetOpacity(Mathf.Lerp(1, 0, failTimeDelta / failMessageDuration));

        if (failTimeDelta > failMessageDuration)
        {
            hasFailed = false;
            failDisplay.gameObject.SetActive(false);
        }
    }
}
