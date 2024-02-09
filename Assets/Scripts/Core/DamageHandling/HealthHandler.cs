using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthHandler : MonoBehaviour
{

    public UnityEvent<int, int> OnHealthChanged;

    // Damage data of the killing hit
    public UnityEvent<DamageData> OnEntityDied;

    public bool IsCharacterDead;

    public int MaxHealth { get => _maxHealth; }
    [SerializeField][Min(1)] private int _maxHealth;

    public int CurrentHealth { get => _currentHealth; }
    private int _currentHealth;


    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;

        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void SetCurrentHealth(int currentHealth)
    {
        _currentHealth = currentHealth;

        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void RestoreHealth()
    {
        _currentHealth = _maxHealth;

        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void DealDamage(DamageData damageData)
    {
        _currentHealth -= damageData.damageAmount;

        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
            OnEntityDied?.Invoke(damageData);
    }


}
