using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class EnemyHealthBar : MonoBehaviour
{
    // public Image healthBarBackground;
    public Image healthBarFill;
    public Transform parentTransform;

    // [SerializeField]
    // private GameObject[] debuffPrefabs;
    [SerializeField]
    private GameObject[] debuffBoxes;
    private List<DebuffSO> debuffs = new List<DebuffSO>();

    public void UpdateHealth()
    {
        healthBarFill.fillAmount =
            GetComponentInParent<HealthController>().RemainingHealthPercentage;
    }

    public void AddDebuff(DebuffSO d)
    {
        debuffs.Add(d);
        RefreshDebuffs();
    }

    public void RemoveDebuff(DebuffSO d)
    {
        debuffs.Remove(d);
        RefreshDebuffs();
    }

    private void RefreshDebuffs()
    {
        foreach (var box in debuffBoxes)
        {
            box.SetActive(false);
        }
        for (int i = 0; i < debuffs.Count; i++)
        {
            debuffBoxes[i].SetActive(true);
            debuffBoxes[i].GetComponent<Image>().sprite = debuffs.ElementAt(i).icon;
        }
    }
}
