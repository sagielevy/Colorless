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

    public void AddItem(CallbackEventData data)
    {
        var itemData = data as ItemEventData;

        // TODO what's needed here?
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
        var placePos = itemData.item.FindProperty("OriginalPosition").vector3Value;
        itemData.gameObject.transform.position = placePos;

        itemData.gameObject.transform.parent = GetRoom().transform;

    }

    private GameObject GetRoom()
    {
        switch (currentRoomIndex)
        {
            case 0:
                return room1;
            case 1:
                return room2;
            case 2:
                return roomFinal;
            default:
                Debug.LogError($"no such room index: {currentRoomIndex}");
                return null;
        }
    }

    public void ChangedRoom(int roomIndex)
    {
        currentRoomIndex = roomIndex;
    }
}
