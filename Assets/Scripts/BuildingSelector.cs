using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSelector : MonoBehaviour
{
    private bool isSelected = false;
    public SpriteOutline outline;

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!isSelected)
        {
            // SelectBuilding();
            outline.enabled = true;
            isSelected = true;
        }
        else
        {
            // DeselectBuilding();
            isSelected = false;
            outline.enabled = false;
        }
    }
}
