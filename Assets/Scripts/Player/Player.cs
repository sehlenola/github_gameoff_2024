using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    // Player properties
    public int MaxRerolls { get; private set; }
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public int CurrentRerolls { get; private set; }

    // List of abilities the player has (maximum of 6)
    public Ability defaultAbility;
    public List<Ability> Abilities { get; private set; } = new List<Ability>();

    // Events to notify UI or other systems of changes
    public delegate void PlayerHealthChanged(int currentHealth, int maxHealth);
    public event PlayerHealthChanged OnPlayerHealthChanged;

    public delegate void PlayerRerollsChanged(int currentRerolls, int maxRerolls);
    public event PlayerRerollsChanged OnPlayerRerollsChanged;

    public delegate void PlayerAbilitiesChanged();
    public event PlayerAbilitiesChanged OnPlayerAbilitiesChanged;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize player stats
        MaxHealth = 100; // Example value
        CurrentHealth = MaxHealth;
        MaxRerolls = 3; // Example value
        CurrentRerolls = MaxRerolls;

        // Initialize abilities if needed
        Abilities.Add(defaultAbility);
    }

    // Methods to modify health
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0)
            CurrentHealth = 0;

        OnPlayerHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth == 0)
        {
            // Handle player death
            Debug.Log("Player has died.");
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        OnPlayerHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    // Methods to modify rerolls
    public void UseReroll()
    {
        if (CurrentRerolls > 0)
        {
            CurrentRerolls--;
            OnPlayerRerollsChanged?.Invoke(CurrentRerolls, MaxRerolls);

            // Implement reroll logic
        }
        else
        {
            Debug.Log("No rerolls left.");
        }
    }

    public void ResetRerolls()
    {
        CurrentRerolls = MaxRerolls;
        OnPlayerRerollsChanged?.Invoke(CurrentRerolls, MaxRerolls);
    }

    // Methods to manage abilities
    public bool AddAbility(Ability ability)
    {
        if (Abilities.Count < 6)
        {
            Abilities.Add(ability);
            OnPlayerAbilitiesChanged?.Invoke();
            return true;
        }
        else
        {
            Debug.Log("Ability list is full.");
            return false;
        }
    }

    public void RemoveAbility(Ability ability)
    {
        if (Abilities.Contains(ability))
        {
            Abilities.Remove(ability);
            OnPlayerAbilitiesChanged?.Invoke();
        }
    }

    public void SwapAbility(Ability oldAbility, Ability newAbility)
    {
        int index = Abilities.IndexOf(oldAbility);
        if (index != -1)
        {
            Abilities[index] = newAbility;
            OnPlayerAbilitiesChanged?.Invoke();
        }
    }
}
