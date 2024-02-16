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
    public UnityEvent<float> OnDamaged;
    public UnityEvent OnHealthChanged;
    private int interval = 1;
    private float nextTime = 0;

    private void Update()
    {
        if (Time.time >= nextTime)
        {
            ApplyRegen();
            ApplyDOT();
            nextTime += interval;
        }
    }

    private void ApplyRegen()
    {
        if (_currentHealth == _maximumHealth)
        {
            return;
        }
        var regen = GetComponent<StatModifiers>().HealthRegen;
        _currentHealth += regen;
        OnHealthChanged.Invoke();
        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }

    private void ApplyDOT()
    {
        var DOT = GetComponent<StatModifiers>().DamageOverTime;
        if (_currentHealth == 0)
        {
            return;
        }

        if (IsInvincible)
        {
            return;
        }
        _currentHealth -= DOT;
        OnHealthChanged.Invoke();
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            OnDied.Invoke();
            if (GetComponent<EnemyController>())
            {
                EnemyManager.instance.enemies.Remove(gameObject);
            }
            Destroy(gameObject);
        }
        else
        {
            OnDamaged.Invoke(DOT);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth == 0)
        {
            return;
        }

        if (IsInvincible)
        {
            return;
        }
        damageAmount -= GetComponent<StatModifiers>().Armour;
        damageAmount *= GetComponent<StatModifiers>().DamageTakenModifier;
        if (damageAmount < 1)
        {
            damageAmount = 1;
        }
        _currentHealth -= damageAmount;
        OnHealthChanged.Invoke();

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            OnDied.Invoke();
            if (GetComponent<EnemyController>())
            {
                var exp = GetComponent<EnemyController>().data.exp;
                var player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Experience>().GainExp(exp);
                EnemyManager.instance.enemies.Remove(gameObject);
                Destroy(gameObject);
            }
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
