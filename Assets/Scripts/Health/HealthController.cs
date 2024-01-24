using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;
    public float RemainingHealthPercentage
    {
        get { return _currentHealth / _maximumHealth; }
    }

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;

    public UnityEvent<int> OnDamaged;
    public UnityEvent OnHealthChanged;

    public void TakeDamage(int damageAmount)
    {
        if (_currentHealth == 0)
        {
            return;
        }

        if (IsInvincible)
        {
            return;
        }
        damageAmount = damageAmount - GetComponent<StatModifiers>().Armour;
        if (damageAmount < 1)
        {
            _currentHealth -= 1;
        }
        else
        {
            damageAmount = damageAmount * GetComponent<StatModifiers>().DamageTakenModifier;
            _currentHealth -= damageAmount;
        }
        OnHealthChanged.Invoke();

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            OnDied.Invoke();
        }
        else
        {
            OnDamaged.Invoke(damageAmount);
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth == _maximumHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;
        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }

    public float GetHealth()
    {
        return _currentHealth;
    }

    public float GetMaxHealth()
    {
        return _maximumHealth;
    }
}
