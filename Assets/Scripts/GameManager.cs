using UnityEngine;
using System.Collections;
using DevionGames.InventorySystem;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private const int itemCount = 4;
    private static GameManager Instance;

    private LevelGenerator levelGenerator;
    private List<string> levelData;

    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private FilterController filterController;

    [SerializeField] private GameWinChecker gameWinChecker;
    [SerializeField] private TMPro.TextMeshProUGUI instructionsText;

    private void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        levelGenerator = new LevelGenerator();

        var itemNames = itemDatabase.items.Select(x => x.name).ToList();

        levelData = levelGenerator.GenerateLevel(itemNames, itemCount);

        UpdateLevel(gameWinChecker, instructionsText);
    }

    public void UpdateLevel(GameWinChecker gameWinChecker, TMPro.TextMeshProUGUI instructionsText)
    {
        gameWinChecker.SetLevelData(levelData.ToHashSet());
        instructionsText.text = levelGenerator.GenerateLevelText(levelData);

        filterController.SetColorlessFactor(0);
        filterController.SetClearColor();
    }

    public void MoveToRoomScene(string roomName)
    {
        SceneManager.LoadScene(roomName);
    }
}
