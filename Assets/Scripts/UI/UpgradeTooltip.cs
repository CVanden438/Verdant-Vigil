using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var data = BuildingManager.instance.highlightedBuilding
            .GetComponent<TowerController>()
            .GetData();
        if (data.tier < 3)
        {
            TooltipManager.instance.ShowTowerTooltip(data.upgrade, transform.position);
        }
        else
        {
            TooltipManager.instance.ShowTowerTooltip(data.maxUpgrades[index], transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.instance.HideTowerTooltip();
    }
}
