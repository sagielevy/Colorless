using DevionGames;
using DevionGames.InventorySystem;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    private int currentRoomIndex = 0;

    private int CurrentRoomIndex
    {
        get { return currentRoomIndex; }
        set
        {
            currentRoomIndex = value;
            ChangeDropMode();
        }
    }

    [SerializeField] private ItemCollection finalRoomItems;
    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject roomFinal;
    [SerializeField] private ItemContainer itemContainer;

    public void AddItem(CallbackEventData data)
    {
        var itemData = data as ItemEventData;

        if (IsInFinalRoom())
        {
            finalRoomItems.Remove(itemData.item);
            ChangeDropMode();
        }
    }

    public void RemoveItemCallback(CallbackEventData data)
    {
        // Happens first on drop out.
        var itemData = data as ItemEventData;

        // TODO Needed?
    }

    public void DropItemCallback(CallbackEventData data)
    {
        // Happens last on drop out.
        var itemData = data as ItemEventData;
        AttemptToRemoveItem(itemData);
    }

    public void UseItemCallback(CallbackEventData data)
    {
        var itemData = data as ItemEventData;
        Debug.Log("use item");

        itemContainer.RemoveItem(itemData.item); // TODO: fix! why is it a prefab?
        AttemptToRemoveItem(itemData);
    }

    private void AttemptToRemoveItem(ItemEventData itemData)
    {
        var originalPos = itemData.item.FindProperty("OriginalPosition").vector3Value;
        var originalRoomInex = itemData.item.FindProperty("OriginalRoomIndex").intValue;
        var finalRoomPos = itemData.item.FindProperty("FinalRoomPosition").vector3Value;

        itemData.gameObject.transform.position = IsInFinalRoom() ?
            finalRoomPos :
            originalPos;

        var roomIndex = IsInFinalRoom() ? GameManager.RoomFinalIndex : originalRoomInex;

        itemData.gameObject.transform.parent = GetRoom(roomIndex).transform;

        if (IsInFinalRoom())
        {
            finalRoomItems.Add(itemData.item);
            ChangeDropMode();
        }
    }

    private bool IsInFinalRoom()
    {
        return CurrentRoomIndex == GameManager.RoomFinalIndex;
    }

    private GameObject GetRoom(int roomIndex)
    {
        switch (roomIndex)
        {
            case GameManager.Room1Index:
                return room1;
            case GameManager.Room2Index:
                return room2;
            case GameManager.RoomFinalIndex:
                return roomFinal;
            default:
                Debug.LogError($"no such room index: {currentRoomIndex}");
                return null;
        }
    }

    public void ChangedRoom(int roomIndex)
    {
        CurrentRoomIndex = roomIndex;
    }

    private void ChangeDropMode()
    {
        var canPlaceItemsInFinalRoom = !IsInFinalRoom() || finalRoomItems.Count < GameManager.ItemGoalCount;
        itemContainer.CanDropItems = canPlaceItemsInFinalRoom;
        itemContainer.CanUseItems = canPlaceItemsInFinalRoom;

        // TODO: Show this when user attempts to use or drop! But this should be in "canUse/canDrop" - they won't be called if these flags are off..
        //InventoryManager.Notifications.roomFull.Show();
    }
}
