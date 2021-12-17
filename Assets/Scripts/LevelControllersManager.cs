using UnityEngine;

public class LevelControllersManager : MonoBehaviour
{
    [SerializeField] private LevelController[] LevelControllers;
    [SerializeField] private DevionGames.InventorySystem.IntVariable level;

    public LevelController GetCurrentLevelController()
    {
        return LevelControllers[level.GetValue()];
    }

    public void SetNextLevel()
    {
        if (GetCurrentLevelController().HasNextLevel)
        {
            level.SetValue(level.GetValue() + 1);
        }
    }
}
