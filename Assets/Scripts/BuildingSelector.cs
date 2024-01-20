using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSelector : MonoBehaviour
{
    private bool isSelected = false;

    // public SpriteOutline outline;
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Color color;

    [SerializeField]
    private Color defaultColor;

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!isSelected)
        {
            // SelectBuilding();
            // outline.enabled = true;
            isSelected = true;
            sr.color = color;
        }
        else
        {
            // DeselectBuilding();
            isSelected = false;
            // outline.enabled = false;
            sr.color = defaultColor;
        }
    }
}
