using System;
using DevionGames.InventorySystem;

public class RoomCapacity : Restriction
{
    public override bool CanAddItem(Item item)
    {
        return true;
    }

    public override bool CanPlaceItemInRoom(int roomIndex, ItemCollection finalRoomItems)
    {
        return !IsInFinalRoom(roomIndex) || finalRoomItems.Count < GameManager.ItemGoalCount;
    }

    private bool IsInFinalRoom(int roomIndex)
    {
        return roomIndex == GameManager.RoomFinalIndex;
    }
}
