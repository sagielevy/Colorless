using UnityEngine;
using System.Collections;
using DevionGames;
using DevionGames.InventorySystem;
using System.Collections.Generic;
using System.Linq;

public class GameWinChecker : MonoBehaviour
{
    [SerializeField] private ItemCollection finalRoom;
    [SerializeField] private GameObject winDisplay;
    [SerializeField] private TMPro.TextMeshProUGUI instructionsText;
    [SerializeField] private FilterController filterController;
    [SerializeField] private float animateWinSpeed = 0.4f;

    private HashSet<string> levelData;
    private bool hasWon = false;
    private float wonTime;

    public void SetLevelData(HashSet<string> levelData)
    {
        this.levelData = levelData;
    }

    public void UpdateLevel(ItemCollection finalRoom,
        GameObject winDisplay, FilterController filterController)
    {
        this.finalRoom = finalRoom;
        this.winDisplay = winDisplay;
        this.filterController = filterController;
    }

    public void CheckForWin()
    {
        var userItems = finalRoom.Select(x => x.Name).ToHashSet();

        foreach (var item in userItems)
        {
            Debug.Log(item);
        }

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

        instructionsText.enabled = false;
        var factor = Mathf.Lerp(0, 1, (Time.time - wonTime) * animateWinSpeed);
        filterController.SetClearColor();
        filterController.SetColorlessFactor(factor);
    }
}
