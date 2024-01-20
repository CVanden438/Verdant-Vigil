using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int crystals;
    public int wood;
    public int coins;
    public UIManager uiManager;

    public void Start()
    {
        uiManager.UpdateCrystalText(crystals);
    }

    public void AddCrystals(int amount)
    {
        crystals += amount;
        uiManager.UpdateCrystalText(crystals);
    }

    public void RemoveCrystals(int amount)
    {
        crystals -= amount;
        uiManager.UpdateCrystalText(crystals);
    }
}
