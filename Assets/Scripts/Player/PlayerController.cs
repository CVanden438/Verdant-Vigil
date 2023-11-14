using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject safeZone;

    [SerializeField]
    private bool isSafe = true;

    [SerializeField]
    private DayNightCycleWithSunAndMoon dayNightCycle;
    public bool isDead = false;

    void Update()
    {
        if (!isSafe && !dayNightCycle.IsDay)
        {
            isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(safeZone))
        {
            isSafe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(safeZone))
        {
            isSafe = false;
        }
    }
}
