using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text crystalText;
    public TextMeshProUGUI buildingName;
    public GameObject buildingInfo;
    public GameObject upgradeButton;
    public GameObject maxUpgradeButtons;

    void Awake()
    {
        instance = this;
    }

    public void UpdateCrystalText(int value)
    {
        crystalText.text = value.ToString();
    }

    public void ShowInfo()
    {
        buildingInfo.SetActive(true);
    }

    public void HideInfo()
    {
        buildingInfo.SetActive(false);
    }
}
