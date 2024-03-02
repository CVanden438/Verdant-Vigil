using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    public GameObject tooltipPrefab; // Assign this in the Inspector
    private GameObject currentTooltip;

    void Awake()
    {
        instance = this;
    }

    public void ShowTooltip(string message, Vector3 position)
    {
        // if (currentTooltip != null)
        // {
        //     Destroy(currentTooltip);
        // }

        // currentTooltip = Instantiate(tooltipPrefab, position, Quaternion.identity, transform);
        tooltipPrefab.transform.position = position;
        tooltipPrefab.GetComponentInChildren<TextMeshProUGUI>().text = message;
        tooltipPrefab.SetActive(true);
    }

    public void HideTooltip()
    {
        // if (currentTooltip != null)
        // {
        //     Destroy(currentTooltip);
        // }
        tooltipPrefab.SetActive(false);
    }
}
