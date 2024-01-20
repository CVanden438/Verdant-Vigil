using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor,
        notSelectedColor;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    // public void OnDrop(PointerEventData eventData)
    // {
    //     if (transform.childCount == 0)
    //     {
    //         InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
    //         inventoryItem.parentAfterDrag = transform;
    //     }
    // }
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (draggedItem != null)
        {
            InventorySlot destinationSlot = this;

            if (transform.childCount > 0)
            {
                // If the destination slot already has an item, swap places
                InventoryItem existingItem = transform.GetChild(0).GetComponent<InventoryItem>();
                existingItem.parentAfterDrag = draggedItem.parentAfterDrag;
                existingItem.transform.SetParent(draggedItem.parentAfterDrag);
            }

            // Update the parentAfterDrag of the dragged item
            draggedItem.parentAfterDrag = destinationSlot.transform;
            draggedItem.transform.SetParent(destinationSlot.transform);
        }
    }
}
