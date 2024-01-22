using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    [SerializeField]
    private GameObject[] armourSlots;

    [SerializeField]
    private GameObject armourDisplay;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateStats()
    {
        var playerArmour = GetComponent<StatModifiers>().Armour;
        foreach (var armourSlot in armourSlots)
        {
            if (armourSlot.transform.childCount > 0)
            {
                InventoryItem itemInSlot = armourSlot.GetComponentInChildren<InventoryItem>();
                var item = (ArmourSO)itemInSlot.itemData;
                playerArmour += item.defence;
            }
        }
        armourDisplay.GetComponent<TextMeshProUGUI>().text = playerArmour.ToString();
    }
}
