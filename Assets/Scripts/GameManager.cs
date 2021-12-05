using UnityEngine;
using System.Collections;
using DevionGames.InventorySystem;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private const int itemCount = 4;
    private LevelGenerator levelGenerator;

    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private FilterController filterController;
    [SerializeField] private GameWinChecker gameWinChecker;
    [SerializeField] private TMPro.TextMeshProUGUI instructionsText;

    private void Start()
    {
        levelGenerator = new LevelGenerator();

        var itemNames = itemDatabase.items.Select(x => x.name).ToList();

        var level = levelGenerator.GenerateLevel(itemNames, itemCount);

        gameWinChecker.SetLevelData(level.ToHashSet());
        instructionsText.text = levelGenerator.GenerateLevelText(level);

        filterController.SetColorlessFactor(0);
        filterController.SetClearColor();
    }
}
