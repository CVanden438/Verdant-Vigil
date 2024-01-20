using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [SerializeField]
    private List<Image> slots;
    private int selectedSlot = 0;
    private int tempSelectedSlot = 0;

    void Start()
    {
        for (int i = 0; i <= 9; i += 1) { }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedSlot = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedSlot = 1;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            selectedSlot = 2;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            selectedSlot = 3;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            selectedSlot = 4;
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            selectedSlot = 5;
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            selectedSlot = 6;
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            selectedSlot = 7;
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            selectedSlot = 8;
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            selectedSlot = 9;
        }
        if (selectedSlot != tempSelectedSlot)
        {
            slots[tempSelectedSlot].color = Color.gray;
            tempSelectedSlot = selectedSlot;
        }
        slots[selectedSlot].color = Color.blue;
    }

    public void UpdateHotbar(List<InventoryItem> inventoryItems)
    {
        if (inventoryItems.Count < 10)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                slots[i].sprite = inventoryItems[i].itemData.icon;
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                slots[i].sprite = inventoryItems[i].itemData.icon;
            }
        }
    }
}
