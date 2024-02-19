using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public WallSO data;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<HealthController>(out var health))
        {
            health._currentHealth = data.maxHealth;
            health._maximumHealth = data.maxHealth;
        }
    }

    // Update is called once per frame
    void Update() { }

    public void DestroyWall()
    {
        Destroy(gameObject);
    }
}
