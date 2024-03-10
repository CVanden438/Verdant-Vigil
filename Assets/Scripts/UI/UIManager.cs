using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // public TMP_Text crystalText;
    // public TextMeshProUGUI buildingName;
    // public GameObject buildingInfo;
    // public GameObject upgradeButton;
    // public GameObject maxUpgradeButtons;
    public GameObject generalPanel;

    [Header("Tower")]
    public GameObject towerPanel;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI def;
    public TextMeshProUGUI range;
    public TextMeshProUGUI health;
    public TextMeshProUGUI speed;
    public List<GameObject> towerTags;
    public GameObject upgradeButton;
    public GameObject maxUpgradeButton1;
    public GameObject maxUpgradeButton2;

    [Header("General")]
    public TextMeshProUGUI buildingName;
    public Image buildingImage;
    public Image expBar;
    public TextMeshProUGUI level;

    void Awake()
    {
        instance = this;
    }

    // public void UpdateCrystalText(int value)
    // {
    //     crystalText.text = value.ToString();
    // }

    public void UpdateExpBar(float value)
    {
        Debug.Log("VALUEL" + value);
        expBar.fillAmount = value;
    }

    public void UpdateLevel(int value)
    {
        level.text = value.ToString();
    }

    public void ShowTowerPanel(TowerSO data)
    {
        Debug.Log("Tower tier" + data.tier);
        upgradeButton.SetActive(false);
        maxUpgradeButton1.SetActive(false);
        maxUpgradeButton2.SetActive(false);
        if (data.tier < 3)
        {
            upgradeButton.SetActive(true);
        }
        if (data.tier == 3)
        {
            Debug.Log("GET HERE");
            maxUpgradeButton1.SetActive(true);
            maxUpgradeButton2.SetActive(true);
        }
        buildingName.text = data.buildingName;
        buildingImage.sprite = data.sprite;
        atk.text = data.damage.ToString();
        range.text = data.attackRange.ToString();
        // def.text = data.a;
        speed.text = data.attackCooldown.ToString();
        health.text = data.maxHealth.ToString();
        foreach (var tag in towerTags)
        {
            tag.SetActive(false);
        }
        for (var i = 0; i < data.tags.Count; i++)
        {
            towerTags[i].GetComponentInChildren<TextMeshProUGUI>().text = data.tags[i].ToString();
            towerTags[i].SetActive(true);
        }
        towerPanel.SetActive(true);
    }

    public void HideTowerPanel()
    {
        // buildingInfo.SetActive(false);
        towerPanel.SetActive(false);
    }
}
