using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;

    [Header("Tower")]
    public GameObject towerTooltipPrefab;
    public TextMeshProUGUI towerName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI def;
    public TextMeshProUGUI range;
    public TextMeshProUGUI health;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI coinCost;
    public TextMeshProUGUI woodCost;
    public TextMeshProUGUI ignotCost;
    public List<GameObject> tags;

    void Awake()
    {
        instance = this;
    }

    public void ShowTowerTooltip(TowerSO data, Vector3 position)
    {
        towerTooltipPrefab.transform.position = position;
        towerName.text = data.buildingName;
        description.text = data.description;
        atk.text = data.damage.ToString();
        range.text = data.attackRange.ToString();
        // def.text = data.;
        speed.text = data.attackCooldown.ToString();
        health.text = data.maxHealth.ToString();
        foreach (ResourceCost resourceCost in data.cost)
        {
            if (resourceCost.resourceName.resourceName == Resources.gold)
            {
                coinCost.text = resourceCost.resourceAmount.ToString();
            }
            if (resourceCost.resourceName.resourceName == Resources.wood)
            {
                woodCost.text = resourceCost.resourceAmount.ToString();
            }
            if (resourceCost.resourceName.resourceName == Resources.ingots)
            {
                ignotCost.text = resourceCost.resourceAmount.ToString();
            }
        }
        // coinCost.text = data.cost.
        // foreach (var tag in tags)
        // {
        //     tag.SetActive(false);
        // }
        // for (var i = 0; i < data.tags.Count; i++)
        // {
        //     tags[i].GetComponentInChildren<TextMeshProUGUI>().text = data.tags[i].ToString();
        //     tags[i].SetActive(true);
        // }
        towerTooltipPrefab.SetActive(true);
        LockToScreen(towerTooltipPrefab);
    }

    public void HideTowerTooltip()
    {
        towerTooltipPrefab.SetActive(false);
    }

    void LockToScreen(GameObject obj)
    {
        var uiElement = obj.GetComponent<RectTransform>();
        // Get the corners of the UI element
        Vector3[] corners = new Vector3[4];
        uiElement.GetWorldCorners(corners);

        // Convert corners to screen space if necessary (if you are using Screen Space - Camera or World Space)
        // for (int i = 0; i < corners.Length; i++)
        // {
        //     if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        //     {
        //         corners[i] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[i]);
        //     }
        // }

        // Check if the UI element is fully visible within the screen
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        bool isFullyVisible = true;
        foreach (Vector3 corner in corners)
        {
            if (!screenRect.Contains(corner))
            {
                isFullyVisible = false;
                break;
            }
        }
        Debug.Log(isFullyVisible);
        if (!isFullyVisible)
        {
            // Calculate a new position to move the element into the screen fully
            Vector2 newPosition = uiElement.anchoredPosition;

            // Adjust the x position if necessary
            if (corners[0].x < 0f) // Left edge is off-screen
            {
                newPosition.x -= corners[0].x;
            }
            else if (corners[3].x > Screen.width) // Right edge is off-screen
            {
                newPosition.x -= corners[3].x - Screen.width;
            }

            // Adjust the y position if necessary
            if (corners[0].y < 0f) // Bottom edge is off-screen
            {
                newPosition.y -= corners[0].y;
            }
            else if (corners[1].y > Screen.height) // Top edge is off-screen
            {
                newPosition.y -= corners[1].y - Screen.height;
            }

            // Apply the new position
            uiElement.anchoredPosition = newPosition;
        }
    }
}
