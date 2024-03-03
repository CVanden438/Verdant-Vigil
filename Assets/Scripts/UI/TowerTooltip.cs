using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TowerSO data;

    //
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.instance.ShowTowerTooltip(data, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.instance.HideTowerTooltip();
    }
}
