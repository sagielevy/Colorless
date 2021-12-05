using UnityEngine;
using System.Collections;
using DevionGames;
using DevionGames.InventorySystem;
using System.Collections.Generic;
using System.Linq;

public class GameWinChecker : MonoBehaviour
{
    [SerializeField] private ItemCollection userInventory;
    [SerializeField] private GameObject WinDisplay;
    [SerializeField] private FilterController filterController;
    [SerializeField] private float animateWinSpeed = 0.4f;

    private HashSet<string> levelData;
    private bool hasWon = false;
    private float wonTime;

    public void SetLevelData(HashSet<string> levelData)
    {
        this.levelData = levelData;
    }

    // Version 0: check that all correct items have been picked up.
    public void CheckForWin()
    {
        var userItems = userInventory.Select(x => x.Name).ToHashSet();

        foreach (var item in userItems)
        {
            Debug.Log(item);
        }

        if (userItems.SetEquals(levelData))
        {
            hasWon = true;
            wonTime = Time.time;
            WinDisplay.SetActive(true);
        }
    }

    private void Update()
    {
        if (!hasWon) return;

        var factor = Mathf.Lerp(0, 1, (Time.time - wonTime) * animateWinSpeed);
        filterController.SetClearColor();
        filterController.SetColorlessFactor(factor);
    }
}
