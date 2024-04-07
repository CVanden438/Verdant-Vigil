using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class HealthBarUI : MonoBehaviour
{
    public Image healthBarFill;
    public GameObject container;

    [SerializeField]
    private GameObject[] debuffBoxes;
    private List<DebuffSO> debuffs = new List<DebuffSO>();

    [SerializeField]
    private GameObject[] buffBoxes;
    private List<BuffSO> buffs = new List<BuffSO>();

    void Start()
    {
        if (GetComponentInParent<HealthController>())
        {
            var health = GetComponentInParent<HealthController>();
            health.OnHealthChanged += UpdateHealth;
            UpdateHealth();
        }
        if (GetComponentInParent<BuffController>())
        {
            var buff = GetComponentInParent<BuffController>();
            buff.OnBuffAdd += AddBuff;
            buff.OnBuffRemove += RemoveBuff;
        }
        if (GetComponentInParent<DebuffController>())
        {
            var debuff = GetComponentInParent<DebuffController>();
            debuff.OnDebuffAdd += AddDebuff;
            debuff.OnDebuffRemove += RemoveDebuff;
        }
    }

    public void UpdateHealth()
    {
        var health = GetComponentInParent<HealthController>().RemainingHealthPercentage;
        healthBarFill.fillAmount = health;
        //Turn healthbar off if full health
        if (health == 1)
        {
            container.SetActive(false);
        }
        else
        {
            container.SetActive(true);
        }
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

    public void AddBuff(BuffSO b)
    {
        buffs.Add(b);
        RefreshBuffs();
    }

    public void RemoveBuff(BuffSO b)
    {
        buffs.Remove(b);
        RefreshBuffs();
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

    private void RefreshBuffs()
    {
        foreach (var box in buffBoxes)
        {
            box.SetActive(false);
        }
        for (int i = 0; i < buffs.Count; i++)
        {
            buffBoxes[i].SetActive(true);
            buffBoxes[i].GetComponent<Image>().sprite = buffs.ElementAt(i).icon;
        }
    }
}
