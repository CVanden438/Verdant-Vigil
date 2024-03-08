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
    public Image expBar;
    public TextMeshProUGUI level;

    void Awake()
    {
        instance = this;
    }

    public void UpdateCrystalText(int value)
    {
        crystalText.text = value.ToString();
    }

    public void UpdateExpBar(float value)
    {
        Debug.Log("VALUEL" + value);
        expBar.fillAmount = value;
    }

    public void UpdateLevel(int value)
    {
        level.text = value.ToString();
    }

    public void ShowTowerPanel()
    {
        buildingInfo.SetActive(true);
    }

    public void HideTowerPanel()
    {
        buildingInfo.SetActive(false);
    }
}
