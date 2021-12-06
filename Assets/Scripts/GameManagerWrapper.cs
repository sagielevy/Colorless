using UnityEngine;
using System.Collections;
using DevionGames.InventorySystem;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManagerWrapper : MonoBehaviour
{
    private static GameStateManager Instance;

    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private ItemCollection inventoryItemCollection;
    [SerializeField] private FilterController filterController;

    [SerializeField] private GameWinChecker gameWinChecker;
    [SerializeField] private TMPro.TextMeshProUGUI instructionsText;

    public static GameStateManager GetInstance { get { return Instance; } }


    private void Start()
    {
        if (Instance == null)
        {
            Instance = new GameStateManager(itemDatabase, filterController,
                gameWinChecker, instructionsText, inventoryItemCollection);
        } else
        {
            GetInstance.StartLevel(filterController, gameWinChecker,
                instructionsText, inventoryItemCollection);

            ItemContainer.AddItems("Inventory", inventoryItemCollection.ToArray());
        }
    }

    public void MoveToRoomScene(string roomName)
    {
        SceneManager.LoadScene(roomName);
    }
}
