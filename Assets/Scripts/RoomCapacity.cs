using System;
using DevionGames.InventorySystem;

public class RoomCapacity : Restriction
{
    private LevelControllersManager levelControllersManager;

    protected override void Start()
    {
        base.Start();
        levelControllersManager = FindObjectOfType<LevelControllersManager>();
    }

    public override bool CanAddItem(Item item)
    {
        return true;
    }

    public override bool CanPlaceItemInRoom(int roomIndex, ItemCollection finalRoomItems)
    {
        var level = levelControllersManager.GetCurrentLevelController();
        return !IsInFinalRoom(roomIndex) || finalRoomItems.Count < level.LevelItemGoalCount();
    }

    private bool IsInFinalRoom(int roomIndex)
    {
        return roomIndex == GameManager.RoomFinalIndex;
    }
}
