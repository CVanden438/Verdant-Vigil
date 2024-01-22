using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffDebuffController : MonoBehaviour
{
    public List<DebuffSO> debuffs = new List<DebuffSO>();

    // private List<>
    public UnityEvent<DebuffSO> OnDebuffAdd;
    public UnityEvent<DebuffSO> OnDebuffRemove;

    [SerializeField]
    private List<DebuffSO> immuneDebuffs;

    public void ApplyDebuff(DebuffSO debuff, float duration)
    {
        if (immuneDebuffs.Contains(debuff))
        {
            return;
        }
        switch (debuff.debuffName)
        {
            case "Shock":
                if (!debuffs.Contains(debuff))
                {
                    StartCoroutine(ApplyShock(duration, debuff));
                }
                break;
        }
    }

    IEnumerator ApplyShock(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        // HealthController healthController = gameObject.GetComponent<HealthController>();
        GetComponent<StatModifiers>().DamageTakenModifier = 10;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().DamageTakenModifier = 1;

        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }
}
