using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TowerSO data;
    private RectTransform uiElement;

    void Awake()
    {
        // uiElement = GetComponent<RectTransform>();
        uiElement = TooltipManager.instance.tooltipPrefab.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.instance.ShowTooltip(data.name, transform.position);
        CheckPosition();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        TooltipManager.instance.HideTooltip();
    }

    void CheckPosition()
    {
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
