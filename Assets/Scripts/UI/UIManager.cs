using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text crystalText;

    public void UpdateCrystalText(int value)
    {
        crystalText.text = value.ToString();
    }
}
