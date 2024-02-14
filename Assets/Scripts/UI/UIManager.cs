using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text crystalText;
    public TextMeshProUGUI buildingName;

    void Awake()
    {
        instance = this;
    }

    public void UpdateCrystalText(int value)
    {
        crystalText.text = value.ToString();
    }
}
