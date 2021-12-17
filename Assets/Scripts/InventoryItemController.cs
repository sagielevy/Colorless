using DevionGames;
using DevionGames.InventorySystem;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private ItemCollection finalRoomItems;

    [SerializeField] private ItemContainer itemContainer;
    [SerializeField] private DevionGames.InventorySystem.IntVariable roomIndex;
    [SerializeField] private DevionGames.InventorySystem.IntVariable level;
    [SerializeField] private GameWinChecker gameWinChecker;
    [SerializeField] private LevelControllersManager levelControllersManager;

    public void AddItem(CallbackEventData data)
    {
        var itemData = data as ItemEventData;

        if (IsInFinalRoom())
        {
            finalRoomItems.Remove(itemData.item);
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
        var originalRoomIndex = itemData.item.FindProperty("OriginalRoomIndex").intValue;
        var finalRoomPos = itemData.item.FindProperty("FinalRoomPosition").vector3Value;

        var levelOverrideOriginalRoomIndex = itemData.item.FindProperty($"Level{level.GetValue()}OriginalRoomIndex");

        if (levelOverrideOriginalRoomIndex != null)
        {
            originalRoomIndex = levelOverrideOriginalRoomIndex.intValue;
        }

        var levelOverrideOriginalPos = itemData.item.FindProperty($"Level{level.GetValue()}OriginalPosition");

        if (levelOverrideOriginalPos != null)
        {
            originalPos = levelOverrideOriginalPos.vector3Value;
        }

        itemData.gameObject.transform.position = IsInFinalRoom() ?
            finalRoomPos :
            originalPos;

        var roomIndexToPlaceItem = IsInFinalRoom() ? GameManager.RoomFinalIndex : originalRoomIndex;

        itemData.gameObject.transform.parent = GetRoom(roomIndexToPlaceItem).transform;

        if (roomIndexToPlaceItem == originalRoomIndex && originalRoomIndex != roomIndex.GetValue())
        {
            InventoryManager.Notifications.itemReturnedToOtherRoom.Show(); // TODO: is this useful?
        }

        if (IsInFinalRoom())
        {
            finalRoomItems.Add(itemData.item);
            gameWinChecker.CheckForWin();
        }
    }

    private bool IsInFinalRoom()
    {
        return roomIndex.GetValue() == GameManager.RoomFinalIndex;
    }

    private GameObject GetRoom(int forIndex)
    {
        var levelController = levelControllersManager.GetCurrentLevelController();

        switch (forIndex)
        {
            case GameManager.Room1Index:
                return levelController.GetRoom1();
            case GameManager.Room2Index:
                return levelController.GetRoom2();
            case GameManager.RoomFinalIndex:
                return levelController.GetFinalRoom();
            default:
                Debug.LogError($"no such room index: {forIndex}");
                return null;
        }
    }

    public void ChangedRoom(int value)
    {
        roomIndex.SetValue(value);
    }
}
