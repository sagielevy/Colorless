using DevionGames.InventorySystem;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject Room1;
    [SerializeField] private GameObject Room2;
    [SerializeField] private GameObject FinalRoom;
    [SerializeField] private ItemCollection LevelTargetItems;
    [SerializeField] private bool NextLevel = true;

    private const int DefaultItemGoalCount = 4;

    public GameObject GetRoom1()
    {
        return Room1;
    }

    public GameObject GetRoom2()
    {
        return Room2;
    }

    public GameObject GetFinalRoom()
    {
        return FinalRoom;
    }

    public ItemCollection GetLevelTargetItems()
    {
        return LevelTargetItems ?? null;
    }

    public int LevelItemGoalCount()
    {
        return HasPredefinedTargetItems ? LevelTargetItems.Count : DefaultItemGoalCount;
    }

    public bool HasPredefinedTargetItems { get { return LevelTargetItems != null; } }

    public bool HasNextLevel { get { return NextLevel; } }
}
