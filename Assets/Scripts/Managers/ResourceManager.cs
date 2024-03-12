using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ResourceManager instance;
    public int ingots = 0;
    public int wood = 0;
    public int coins = 0;

    // public UIManager uiManager;

    void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        UIManager.instance.UpdateResources(coins, wood, ingots);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UIManager.instance.UpdateResources(coins, wood, ingots);
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        UIManager.instance.UpdateResources(coins, wood, ingots);
    }

    public void AddWood(int amount)
    {
        wood += amount;
        UIManager.instance.UpdateResources(coins, wood, ingots);
    }

    public void RemoveWood(int amount)
    {
        wood -= amount;
        UIManager.instance.UpdateResources(coins, wood, ingots);
    }

    public void AddIngots(int amount)
    {
        ingots += amount;
        UIManager.instance.UpdateResources(coins, wood, ingots);
    }

    public void RemoveIngots(int amount)
    {
        ingots -= amount;
        UIManager.instance.UpdateResources(coins, wood, ingots);
    }
}
