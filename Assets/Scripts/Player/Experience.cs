using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float exp = 0;
    public int level = 1;
    public float requiredExp = 10;

    // public float percentExp = 0;

    public void GainExp(int expGain)
    {
        Debug.Log("EXP" + expGain);
        exp += expGain;
        UIManager.instance.UpdateExpBar(exp / requiredExp);
        if (exp >= requiredExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        float carryoverExp = exp - requiredExp;
        level += 1;
        UIManager.instance.UpdateLevel(level);
        exp = 0;
        requiredExp = level * 10;
        if (carryoverExp > 0)
        {
            exp += carryoverExp;
            //if enough exp at once for multiple levels
            if (exp >= requiredExp)
            {
                LevelUp();
            }
        }
        UIManager.instance.UpdateExpBar(exp / requiredExp);
    }
}
