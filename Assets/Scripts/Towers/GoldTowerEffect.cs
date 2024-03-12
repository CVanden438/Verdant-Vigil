using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTowerEffect : MonoBehaviour, ITowerEffect
{
    public int coinAmount;

    public void CollisionEffect(Vector3 position)
    {
        ResourceManager.instance.AddCoins(coinAmount);
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
