using System.Collections.Generic;

public class Stat
{
    public float baseValue;
    private float finalValue;

    private List<float> additiveModifiers = new List<float>();
    private List<float> multiplicativeModifiers = new List<float>();

    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
    }

    public void AddModifier(float value)
    {
        additiveModifiers.Add(value);
        CalculateFinalValue();
    }

    public void MultiplyModifier(float value)
    {
        multiplicativeModifiers.Add(value);
        CalculateFinalValue();
    }

    public void RemoveAddModifier(float value)
    {
        additiveModifiers.Remove(value);
        CalculateFinalValue();
    }

    public void RemoveMultiplyModifier(float value)
    {
        multiplicativeModifiers.Remove(value);
        CalculateFinalValue();
    }

    public float GetFinalValue()
    {
        return finalValue;
    }

    private void CalculateFinalValue()
    {
        finalValue = baseValue;

        // Apply additive modifiers
        foreach (float modifier in additiveModifiers)
        {
            finalValue += modifier;
        }

        // Apply multiplicative modifiers
        foreach (float modifier in multiplicativeModifiers)
        {
            finalValue *= modifier;
        }
    }
}
