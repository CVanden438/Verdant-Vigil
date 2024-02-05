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
            case Debuffs.shock:
                //dont apply if already applied
                if (!debuffs.Contains(debuff))
                {
                    StartCoroutine(ApplyShock(duration, debuff));
                }
                break;
            case Debuffs.chill:
                if (!debuffs.Contains(debuff))
                {
                    StartCoroutine(ApplyChill(duration, debuff));
                }
                break;
            case Debuffs.freeze:
                if (!debuffs.Contains(debuff))
                {
                    StartCoroutine(ApplyFreeze(duration, debuff));
                }
                break;
            case Debuffs.stun:
                if (!debuffs.Contains(debuff))
                {
                    StartCoroutine(ApplyStun(duration, debuff));
                }
                break;
            case Debuffs.electrocute:
                if (!debuffs.Contains(debuff))
                {
                    StartCoroutine(ApplyElectrocute(duration, debuff));
                }
                break;
        }
    }

    public void ApplyDOT(DebuffSO debuff, float duration, float amount)
    {
        if (immuneDebuffs.Contains(debuff))
        {
            return;
        }
        switch (debuff.debuffName)
        {
            case Debuffs.bleed:
                if (!debuffs.Contains(debuff))
                {
                    StartCoroutine(ApplyBleed(duration, debuff, amount));
                }
                break;
            case Debuffs.burn:
                if (!debuffs.Contains(debuff))
                {
                    StartCoroutine(ApplyBurn(duration, debuff, amount));
                }
                break;
        }
    }

    IEnumerator ApplyShock(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        GetComponent<StatModifiers>().DamageTakenModifier = 1.2f;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().DamageTakenModifier = 1;
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyChill(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        GetComponent<StatModifiers>().MoveSpeedMultiplier = 0.7f;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().MoveSpeedMultiplier = 1;
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyFreeze(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        GetComponent<StatModifiers>().MoveSpeedMultiplier = 0f;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().MoveSpeedMultiplier = 1;
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyElectrocute(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        GetComponent<StatModifiers>().DamageTakenModifier = 1.4f;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().MoveSpeedMultiplier = 1;
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyBleed(float duration, DebuffSO debuff, float amount)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        GetComponent<StatModifiers>().DamageOverTime += amount;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().DamageOverTime -= amount;
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyBurn(float duration, DebuffSO debuff, float amount)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        GetComponent<StatModifiers>().DamageOverTime += amount;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().DamageOverTime -= amount;
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyStun(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        GetComponent<StatModifiers>().MoveSpeedMultiplier = 0f;
        GetComponent<StatModifiers>().AttackSpeedModifier = 999f;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().MoveSpeedMultiplier = 1;
        GetComponent<StatModifiers>().AttackSpeedModifier = 1;
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }
}
