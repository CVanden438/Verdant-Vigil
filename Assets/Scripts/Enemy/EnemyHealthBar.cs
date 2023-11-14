using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // public Image healthBarBackground;
    public Image healthBarFill;

    public void UpdateHealth()
    {
        healthBarFill.fillAmount = GetComponent<HealthController>().RemainingHealthPercentage;
    }
}
