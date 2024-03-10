using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    // public GameObject highlight;
    public TowerSO data;

    void Start()
    {
        data = GetComponent<TowerController>().GetData();
    }

    void OnMouseDown()
    {
        // highlight.SetActive(true);
        UIManager.instance.ShowTowerPanel(data);
        BuildingManager.instance.highlightedBuilding = gameObject;
    }
}
