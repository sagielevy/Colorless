using System.Collections;
using System.Collections.Generic;
using DevionGames;
using DevionGames.InventorySystem;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public void AddItem(CallbackEventData data)
    {
        var itemData = data as ItemEventData;


    }

    public void RemoveItem(CallbackEventData data)
    {
        // Happens first on drop out.
        var itemData = data as ItemEventData;
    }

    public void DropItem(CallbackEventData data)
    {
        // Happens last on drop out.
        var itemData = data as ItemEventData;
        var originalPos = itemData.item.FindProperty("OriginalPosition").vector3Value;
        itemData.gameObject.transform.position = originalPos;
    }
}
