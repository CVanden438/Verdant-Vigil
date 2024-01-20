using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // [SerializeField]
    // private GameObject safeZone;

    [SerializeField]
    private bool isSafe = true;

    // [SerializeField]
    // private DayNightCycleWithSunAndMoon dayNightCycle;
    public bool isDead = false;
    public Image healthBarFill;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private HealthController health;

    void Update()
    {
        // if (!isSafe && !dayNightCycle.IsDay)
        // {
        //     isDead = true;
        // }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.Equals(safeZone))
    //     {
    //         isSafe = true;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.Equals(safeZone))
    //     {
    //         isSafe = false;
    //     }
    // }

    public void UpdateHealth()
    {
        healthBarFill.fillAmount = GetComponent<HealthController>().RemainingHealthPercentage;
        text.SetText(health.GetHealth() + "/" + health.GetMaxHealth());
    }
}
