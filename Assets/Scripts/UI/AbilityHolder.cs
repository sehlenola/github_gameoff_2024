using UnityEngine;
using System.Collections.Generic;

public class AbilityHolder : MonoBehaviour
{
    public GameObject abilityCardPrefab; // Prefab for the in-game ability cards
    public Transform abilityCardsContainer; // Parent transform for ability cards

    private List<AbilityCard> abilityCards = new List<AbilityCard>();

    private void Start()
    {
        // Subscribe to player's abilities changed event
        Player.Instance.OnPlayerAbilitiesChanged += UpdateAbilityCards;

        // Initialize ability cards
        UpdateAbilityCards();
    }

    private void OnDestroy()
    {
        // Unsubscribe from event
        if (Player.Instance != null)
        {
            Player.Instance.OnPlayerAbilitiesChanged -= UpdateAbilityCards;
        }
    }

    private void UpdateAbilityCards()
    {
        // Clear existing ability cards
        foreach (Transform child in abilityCardsContainer)
        {
            Destroy(child.gameObject);
        }
        abilityCards.Clear();

        // Create ability cards based on player's abilities
        foreach (Ability ability in Player.Instance.Abilities)
        {
            GameObject cardObj = Instantiate(abilityCardPrefab, abilityCardsContainer);
            AbilityCard abilityCard = cardObj.GetComponent<AbilityCard>();
            abilityCard.SetAbility(ability);
            abilityCards.Add(abilityCard);
        }
    }

    // Optional: Method to get a reference to the ability cards
    public List<AbilityCard> GetAbilityCards()
    {
        return abilityCards;
    }
}
