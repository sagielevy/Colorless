using System.Collections;
using System.Collections.Generic;
using DevionGames;
using DevionGames.InventorySystem;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    private int currentRoomIndex = 0;

    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject roomFinal;
    [SerializeField] private ItemContainer itemContainer;

    private int itemsInFinalRoom = 0;

    public void AddItem(CallbackEventData data)
    {
        var itemData = data as ItemEventData;

        if (IsInFinalRoom())
        {
            itemsInFinalRoom--;
        }
    }

    public void RemoveItem(CallbackEventData data)
    {
        // Happens first on drop out.
        var itemData = data as ItemEventData;

        // TODO what's needed here?
    }

    public void DropItem(CallbackEventData data)
    {
        // Happens last on drop out.
        var itemData = data as ItemEventData;


        var originalPos = itemData.item.FindProperty("OriginalPosition").vector3Value;
        var finalRoomPos = itemData.item.FindProperty("FinalRoomPosition").vector3Value;

        itemData.gameObject.transform.position = IsInFinalRoom() ?
            finalRoomPos :
            originalPos;

        itemData.gameObject.transform.parent = GetRoom().transform;

        if (IsInFinalRoom())
        {
            itemsInFinalRoom++;
        }
    }

    private bool IsInFinalRoom()
    {
        return currentRoomIndex == GameStateManager.RoomFinalIndex;
    }

    private GameObject GetRoom()
    {
        switch (currentRoomIndex)
        {
            case GameStateManager.Room1Index:
                return room1;
            case GameStateManager.Room2Index:
                return room2;
            case GameStateManager.RoomFinalIndex:
                return roomFinal;
            default:
                Debug.LogError($"no such room index: {currentRoomIndex}");
                return null;
        }
    }

    public void ChangedRoom(int roomIndex)
    {
        currentRoomIndex = roomIndex;
        ChangeDropMode();
    }

    private void ChangeDropMode()
    {
        if (IsInFinalRoom() && itemsInFinalRoom >= GameStateManager.ItemGoalCount)
        {
            itemContainer.CanDropItems = false;
        } else
        {
            itemContainer.CanDropItems = true;
        }
    }
}
