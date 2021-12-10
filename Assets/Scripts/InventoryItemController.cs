using DevionGames;
using DevionGames.InventorySystem;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    //private int currentRoomIndex = 0;

    //public int CurrentRoomIndex
    //{
    //    get { return currentRoomIndex; }
    //    private set
    //    {
    //        currentRoomIndex = value;
    //        SetCanPlaceItemsMode();
    //    }
    //}

    [SerializeField] private ItemCollection finalRoomItems;
    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject roomFinal;
    [SerializeField] private ItemContainer itemContainer;
    [SerializeField] private DevionGames.InventorySystem.IntVariable roomIndex;

    public void AddItem(CallbackEventData data)
    {
        var itemData = data as ItemEventData;

        if (IsInFinalRoom())
        {
            finalRoomItems.Remove(itemData.item);
            SetCanPlaceItemsMode();
        }
    }

    public void RemoveItemCallback(CallbackEventData data)
    {
        // Happens first on drop out.
        var itemData = data as ItemEventData;
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

        var roomIndexToPlaceItem = IsInFinalRoom() ? GameManager.RoomFinalIndex : originalRoomInex;

        itemData.gameObject.transform.parent = GetRoom(roomIndexToPlaceItem).transform;

        if (roomIndexToPlaceItem == originalRoomInex && originalRoomInex != roomIndex.GetValue())
        {
            InventoryManager.Notifications.itemReturnedToOtherRoom.Show(); // TODO: is this useful?
        }

        if (IsInFinalRoom())
        {
            finalRoomItems.Add(itemData.item);
            SetCanPlaceItemsMode();
        }
    }

    private bool IsInFinalRoom()
    {
        return roomIndex.GetValue() == GameManager.RoomFinalIndex;
    }

    private GameObject GetRoom(int forIndex)
    {
        switch (forIndex)
        {
            case GameManager.Room1Index:
                return room1;
            case GameManager.Room2Index:
                return room2;
            case GameManager.RoomFinalIndex:
                return roomFinal;
            default:
                Debug.LogError($"no such room index: {forIndex}");
                return null;
        }
    }

    public void ChangedRoom(int value)
    {
        roomIndex.SetValue(value);
        SetCanPlaceItemsMode();
    }

    private bool CanPlaceItemsInRoom()
    {
        return !IsInFinalRoom() || finalRoomItems.Count < GameManager.ItemGoalCount;
    }

    private void SetCanPlaceItemsMode()
    {
        var canPlaceItemsInFinalRoom = CanPlaceItemsInRoom();
        itemContainer.CanDropItems = canPlaceItemsInFinalRoom;
        itemContainer.CanUseItems = canPlaceItemsInFinalRoom;

        // TODO: Show this when user attempts to use or drop! But this should be in "canUse/canDrop" - they won't be called if these flags are off..
        //InventoryManager.Notifications.roomFull.Show();
    }
}
