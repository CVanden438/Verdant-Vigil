using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffController : MonoBehaviour
{
    public List<BuffSO> buffs = new List<BuffSO>();

    // private List<>
    public UnityEvent<BuffSO> OnBuffAdd;
    public UnityEvent<BuffSO> OnBuffRemove;

    public void ApplyBuff(BuffSO buff, float duration)
    {
        if (buffs.Contains(buff))
        {
            return;
        }
        switch (buff.buffName)
        {
            case Buffs.speedy:
                StartCoroutine(ApplySpeedy(duration, buff));
                break;
            case Buffs.enraged:
                StartCoroutine(ApplyEnraged(duration, buff));
                break;
        }
    }

    public void ApplyRegen(BuffSO buff, float duration, float amount)
    {
        // switch (buff.buffName) { }
    }

    IEnumerator ApplySpeedy(float duration, BuffSO buff)
    {
        buffs.Add(buff);
        OnBuffAdd.Invoke(buff);
        GetComponent<StatModifiers>().MoveSpeedMultiplier = 1.2f;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().MoveSpeedMultiplier = 1;
        buffs.Remove(buff);
        OnBuffRemove.Invoke(buff);
    }

    IEnumerator ApplyEnraged(float duration, BuffSO buff)
    {
        buffs.Add(buff);
        OnBuffAdd.Invoke(buff);
        GetComponent<StatModifiers>().AttackSpeedModifier = 1.2f;
        GetComponent<StatModifiers>().DamageModifier = 1.2f;
        yield return new WaitForSeconds(duration);
        GetComponent<StatModifiers>().AttackSpeedModifier = 1f;
        GetComponent<StatModifiers>().DamageModifier = 1f;
        buffs.Remove(buff);
        OnBuffRemove.Invoke(buff);
    }
}
