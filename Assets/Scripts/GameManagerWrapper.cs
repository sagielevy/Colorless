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
    [SerializeField] private ItemCollection finalRoomItems;
    [SerializeField] private FilterController filterController;

    [SerializeField] private GameWinChecker gameWinChecker;
    [SerializeField] private TMPro.TextMeshProUGUI instructionsText;

    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject roomFinal;

    [SerializeField] private Material colorlessMaterial;

    public static GameStateManager GetInstance { get { return Instance; } }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = new GameStateManager(itemDatabase, filterController,
                gameWinChecker, instructionsText);

#if UNITY_EDITOR
            colorlessMaterial.SetVector("_MouseOrientation", new Vector4(1, -1, 0, 0));
#elif UNITY_WEBGL
            // Don't flip for web.
            colorlessMaterial.SetVector("_MouseOrientation", new Vector4(1, 1, 0, 0));
#else
            colorlessMaterial.SetVector("_MouseOrientation", new Vector4(1, -1, 0, 0));
#endif
        }
    }

    public void MoveToRoom(int roomIndex)
    {
        switch (roomIndex)
        {
            case GameStateManager.Room1Index:
                room1.SetActive(true);
                room2.SetActive(false);
                roomFinal.SetActive(false);
                GetInstance.EnterRoom(filterController, gameWinChecker,
                    instructionsText);
                break;
            case GameStateManager.Room2Index:
                room1.SetActive(false);
                room2.SetActive(true);
                roomFinal.SetActive(false);
                GetInstance.EnterRoom(filterController, gameWinChecker,
                    instructionsText);
                break;
            case GameStateManager.RoomFinalIndex:
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
