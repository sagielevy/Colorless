using UnityEngine;
using DevionGames.InventorySystem;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public const int Room1Index = 0;
    public const int Room2Index = 1;
    public const int RoomFinalIndex = 2;

    private List<string> levelData;

    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private FilterController filterController;

    [SerializeField] private GameWinChecker gameWinChecker;
    [SerializeField] private TMPro.TextMeshProUGUI instructionsText;
    [SerializeField] private LevelControllersManager levelControllersManager; 
    [SerializeField] private Material colorlessMaterial;
    [SerializeField] private DevionGames.InventorySystem.IntVariable roomIndex;
    [SerializeField] private DevionGames.InventorySystem.IntVariable level;

    private void Start()
    {
        DOTween.KillAll();

        SetupCurrentLevel();
        SetupLevelData();

        gameWinChecker.SetLevelData(levelData.ToHashSet());
        instructionsText.text = new LevelGenerator().GenerateLevelText(levelData);

        filterController.SetColorlessFactor(0);
        filterController.SetClearColor();
    }

    private void SetupCurrentLevel()
    {
        var levelController = levelControllersManager.GetCurrentLevelController();
        levelController.gameObject.SetActive(true);
        roomIndex.SetValue(Room1Index);
    }

    private void SetupLevelData()
    {
        var levelController = levelControllersManager.GetCurrentLevelController();

        if (levelController.HasPredefinedTargetItems)
        {
            levelData = levelController.GetLevelTargetItems().Select(x => x.DisplayName).ToList();
        }
        else
        {
            var level = levelControllersManager.GetCurrentLevelController();
            var levelGenerator = new LevelGenerator();
            var itemNames = itemDatabase.items.Select(x => x.DisplayName).ToList();
            levelData = levelGenerator.GenerateLevel(itemNames, level.LevelItemGoalCount());
        }
    }

    public void MoveToRoom(int roomIndex)
    {
        var levelController = levelControllersManager.GetCurrentLevelController();

        switch (roomIndex)
        {
            case Room1Index:
                levelController.GetRoom1().SetActive(true);
                if (levelController.GetRoom2() != null) levelController.GetRoom2().SetActive(false);
                levelController.GetFinalRoom().SetActive(false);

                filterController.SetColorlessFactor(0);
                filterController.SetClearColor();
                break;
            case Room2Index:
                if (levelController.GetRoom1() != null) levelController.GetRoom1().SetActive(false);
                levelController.GetRoom2().SetActive(true);
                levelController.GetFinalRoom().SetActive(false);
                filterController.SetColorlessFactor(0);
                filterController.SetClearColor();
                break;
            case RoomFinalIndex:
                if (levelController.GetRoom1() != null) levelController.GetRoom1().SetActive(false);
                if (levelController.GetRoom2() != null) levelController.GetRoom2().SetActive(false);
                levelController.GetFinalRoom().SetActive(true);
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
