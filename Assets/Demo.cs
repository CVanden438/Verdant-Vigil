using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemSO[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("ITEM ADDED");
        }
        else
        {
            Debug.Log("Item NOT ADDED");
        }
    }

    public void GetSelectedItem()
    {
        ItemSO recievedItem = inventoryManager.GetSelectedItem(false);
        if (recievedItem != null)
        {
            Debug.Log("Recieved item");
        }
        else
        {
            Debug.Log("NO item recieved");
        }
    }

    public void UseSelectedItem()
    {
        ItemSO recievedItem = inventoryManager.GetSelectedItem(true);
        if (recievedItem != null)
        {
            Debug.Log("Recieved item");
        }
        else
        {
            Debug.Log("NO item recieved");
        }
    }
}
