﻿using UnityEngine;
using System.Collections;
using DevionGames.InventorySystem;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManagerWrapper : MonoBehaviour
{
    private static GameStateManager Instance;

    [SerializeField] private ItemDatabase itemDatabase;
    //[SerializeField] private ItemCollection inventoryItemCollection;
    [SerializeField] private FilterController filterController;

    [SerializeField] private GameWinChecker gameWinChecker;
    [SerializeField] private TMPro.TextMeshProUGUI instructionsText;

    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject roomFinal;

    public static GameStateManager GetInstance { get { return Instance; } }


    private void Start()
    {
        if (Instance == null)
        {
            Instance = new GameStateManager(itemDatabase, filterController,
                gameWinChecker, instructionsText);
        }
        //else
        //{
            //GetInstance.EnterRoom(filterController, gameWinChecker,
            //    instructionsText);

            //ItemContainer.AddItems("Inventory", inventoryItemCollection.ToArray());
        //}
    }

    //public void MoveToRoomScene(string roomName)
    //{
    //    SceneManager.LoadScene(roomName);
    //}

    public void MoveToRoom(int roomIndex)
    {
        switch (roomIndex)
        {
            case 0:
                room1.SetActive(true);
                room2.SetActive(false);
                roomFinal.SetActive(false);
                GetInstance.EnterRoom(filterController, gameWinChecker,
                    instructionsText);
                break;
            case 1:
                room1.SetActive(false);
                room2.SetActive(true);
                roomFinal.SetActive(false);
                GetInstance.EnterRoom(filterController, gameWinChecker,
                    instructionsText);
                break;
            case 2:
                room1.SetActive(false);
                room2.SetActive(false);
                roomFinal.SetActive(true);
                GetInstance.EnterRoom(filterController, gameWinChecker,
                    instructionsText);
                break;
            default:
                Debug.LogError($"no such room index: {roomIndex}");
                break;
        }
    }
}
