using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebuffController : MonoBehaviour
{
    public List<DebuffSO> debuffs = new List<DebuffSO>();

    // private List<>
    public delegate void DebuffAddAction(DebuffSO debuff);
    public delegate void DebuffRemoveAction(DebuffSO debuff);
    public event DebuffAddAction OnDebuffAdd;
    public event DebuffRemoveAction OnDebuffRemove;
    StatModifiers sm;

    [SerializeField]
    private List<DebuffSO> immuneDebuffs;

    void Awake()
    {
        sm = GetComponent<StatModifiers>();
    }

    public void ApplyDebuff(DebuffSO debuff, float duration)
    {
        if (immuneDebuffs.Contains(debuff))
        {
            return;
        }
        if (debuffs.Contains(debuff))
        {
            return;
        }
        switch (debuff.debuffName)
        {
            case Debuffs.shock:
                StartCoroutine(ApplyShock(duration, debuff));
                break;
            case Debuffs.chill:
                StartCoroutine(ApplyChill(duration, debuff));
                break;
            case Debuffs.freeze:
                StartCoroutine(ApplyFreeze(duration, debuff));
                break;
            case Debuffs.stun:
                StartCoroutine(ApplyStun(duration, debuff));
                break;
            case Debuffs.electrocute:
                StartCoroutine(ApplyElectrocute(duration, debuff));
                break;
        }
    }

    public void ApplyDOT(DebuffSO debuff, float duration, float amount)
    {
        if (immuneDebuffs.Contains(debuff))
        {
            return;
        }
        if (debuffs.Contains(debuff))
        {
            return;
        }
        switch (debuff.debuffName)
        {
            case Debuffs.bleed:
                StartCoroutine(ApplyBleed(duration, debuff, amount));
                break;
            case Debuffs.burn:
                StartCoroutine(ApplyBurn(duration, debuff, amount));
                break;
        }
    }

    IEnumerator ApplyShock(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        sm.DamageTakenModifier.MultiplyModifier(1.2f);
        yield return new WaitForSeconds(duration);
        sm.DamageTakenModifier.RemoveMultiplyModifier(1.2f);
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyChill(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        sm.MoveSpeedModifier.MultiplyModifier(0.7f);
        yield return new WaitForSeconds(duration);
        sm.MoveSpeedModifier.RemoveMultiplyModifier(0.7f);
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyFreeze(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        sm.MoveSpeedModifier.MultiplyModifier(0);
        yield return new WaitForSeconds(duration);
        sm.MoveSpeedModifier.RemoveMultiplyModifier(0);
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyElectrocute(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        sm.DamageTakenModifier.MultiplyModifier(1.4f);
        yield return new WaitForSeconds(duration);
        sm.DamageTakenModifier.RemoveMultiplyModifier(1.4f);
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyStun(float duration, DebuffSO debuff)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        sm.MoveSpeedModifier.MultiplyModifier(0);
        sm.AttackSpeedModifier.MultiplyModifier(999);
        yield return new WaitForSeconds(duration);
        sm.MoveSpeedModifier.RemoveMultiplyModifier(0);
        sm.AttackSpeedModifier.RemoveMultiplyModifier(999);
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    //-----------------DOT------------------
    IEnumerator ApplyBleed(float duration, DebuffSO debuff, float amount)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        sm.DamageOverTime.AddModifier(amount);
        yield return new WaitForSeconds(duration);
        sm.DamageOverTime.RemoveAddModifier(amount);
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }

    IEnumerator ApplyBurn(float duration, DebuffSO debuff, float amount)
    {
        debuffs.Add(debuff);
        OnDebuffAdd.Invoke(debuff);
        sm.DamageOverTime.AddModifier(amount);
        yield return new WaitForSeconds(duration);
        sm.DamageOverTime.RemoveAddModifier(amount);
        debuffs.Remove(debuff);
        OnDebuffRemove.Invoke(debuff);
    }
}
