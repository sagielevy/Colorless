using UnityEngine;
using System.Collections;
using DevionGames.InventorySystem;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public const int Room1Index = 0;
    public const int Room2Index = 1;
    public const int RoomFinalIndex = 2;
    public const int ItemGoalCount = 4;

    private List<string> levelData;

    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private FilterController filterController;

    [SerializeField] private GameWinChecker gameWinChecker;
    [SerializeField] private TMPro.TextMeshProUGUI instructionsText;

    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject roomFinal;

    [SerializeField] private Material colorlessMaterial;

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        var levelGenerator = new LevelGenerator();

        var itemNames = itemDatabase.items.Select(x => x.Name).ToList();

        levelData = levelGenerator.GenerateLevel(itemNames, ItemGoalCount);

        gameWinChecker.SetLevelData(levelData.ToHashSet());
        instructionsText.text = new LevelGenerator().GenerateLevelText(levelData);

        filterController.SetColorlessFactor(0);
        filterController.SetClearColor();

#if UNITY_EDITOR
        colorlessMaterial.SetVector("_MouseOrientation", new Vector4(1, -1, 0, 0));
#elif UNITY_WEBGL
            // Don't flip for web.
            colorlessMaterial.SetVector("_MouseOrientation", new Vector4(1, 1, 0, 0));
#else
            colorlessMaterial.SetVector("_MouseOrientation", new Vector4(1, -1, 0, 0));
#endif
    }

    public void MoveToRoom(int roomIndex)
    {
        switch (roomIndex)
        {
            case Room1Index:
                room1.SetActive(true);
                room2.SetActive(false);
                roomFinal.SetActive(false);
                filterController.SetColorlessFactor(0);
                filterController.SetClearColor();
                break;
            case Room2Index:
                room1.SetActive(false);
                room2.SetActive(true);
                roomFinal.SetActive(false);
                filterController.SetColorlessFactor(0);
                filterController.SetClearColor();
                break;
            case RoomFinalIndex:
                room1.SetActive(false);
                room2.SetActive(false);
                roomFinal.SetActive(true);
                filterController.SetColorlessFactor(0);
                filterController.SetClearColor();
                break;
            default:
                Debug.LogError($"no such room index: {roomIndex}");
                break;
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(0);
    }
}
