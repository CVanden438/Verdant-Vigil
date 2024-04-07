using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffController : MonoBehaviour
{
    public List<BuffSO> buffs = new List<BuffSO>();
    public delegate void BuffAddAction(BuffSO buff);
    public delegate void BuffRemoveAction(BuffSO buff);
    public event BuffAddAction OnBuffAdd;
    public event BuffRemoveAction OnBuffRemove;
    SpriteRenderer sr;
    StatModifiers sm;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sm = GetComponent<StatModifiers>();
    }

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
            case Buffs.elite:
                ApplyElite();
                break;
        }
    }

    // public void ApplyRegen(BuffSO buff, float duration, float amount)
    // {
    //     switch (buff.buffName) { }
    // }

    public void ApplyElite()
    {
        sr.color = Color.yellow;
        sr.transform.localScale = transform.localScale * 1.5f;
        sm.DamageModifier.MultiplyModifier(2);
        sm.MaxHealthModifier.MultiplyModifier(2);
    }

    IEnumerator ApplySpeedy(float duration, BuffSO buff)
    {
        buffs.Add(buff);
        OnBuffAdd.Invoke(buff);
        sm.MoveSpeedModifier.MultiplyModifier(1.2f);
        yield return new WaitForSeconds(duration);
        sm.MoveSpeedModifier.RemoveMultiplyModifier(1.2f);
        buffs.Remove(buff);
        OnBuffRemove.Invoke(buff);
    }

    IEnumerator ApplyEnraged(float duration, BuffSO buff)
    {
        buffs.Add(buff);
        OnBuffAdd.Invoke(buff);
        sm.AttackSpeedModifier.MultiplyModifier(1.2f);
        sm.DamageModifier.MultiplyModifier(1.2f);
        yield return new WaitForSeconds(duration);
        sm.AttackSpeedModifier.RemoveMultiplyModifier(1.2f);
        sm.DamageModifier.RemoveMultiplyModifier(1.2f);
        buffs.Remove(buff);
        OnBuffRemove.Invoke(buff);
    }
}
