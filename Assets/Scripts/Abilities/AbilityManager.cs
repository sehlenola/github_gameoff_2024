using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }

    public Ability SelectedAbility;
    public AbilityCard abilityCard;
    private List<Tile> highlightedTiles = new List<Tile>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
        else
        {
            Instance = this;
        }
    }

    public void SelectAbility(Ability ability)
    {
        Debug.Log("Selecting ability " + ability);
        SelectedAbility = ability;
        // Update UI to reflect selected ability
    }

    public void SelectCard(AbilityCard abilityCard)
    {
        this.abilityCard = abilityCard;
    }

    public void CancelAbility()
    {
        if (SelectedAbility != null)
        {
            // Return the dice to the AbilityCard
            //AbilityCard abilityCard = FindAbilityCardForAbility(SelectedAbility);
            if (abilityCard != null)
            {
                abilityCard.OnAbilityCancelled();
            }
            SelectedAbility = null;
            ClearHighlightedTiles();
        }
    }

    public void UseAbilityOnTile(Tile targetTile)
    {
        if (SelectedAbility != null)
        {
            if (SelectedAbility.CanActivate(targetTile))
            {
                SelectedAbility.Activate(targetTile);
                if (abilityCard != null)
                {
                    abilityCard.OnAbilityUsed();
                }
                // Deselect ability after use
                SelectedAbility = null;
                abilityCard = null;
                ClearHighlightedTiles();
            }
            else
            {
                Debug.Log("Ability cannot be activated on this tile.");
            }
        }
        else
        {
            Debug.Log("No ability selected.");
        }
    }

    public void HighlightTiles(List<Tile> tiles)
    {
        ClearHighlightedTiles();

        if (tiles != null)
        {
            foreach (Tile tile in tiles)
            {
                tile.SetHighlighted(true);
                highlightedTiles.Add(tile);
            }
        }
    }

    public void ClearHighlightedTiles()
    {
        foreach (Tile tile in highlightedTiles)
        {
            tile.SetHighlighted(false);
        }
        highlightedTiles.Clear();
    }

    private AbilityCard FindAbilityCardForAbility(Ability ability)
    {
        // Implement logic to find the AbilityCard associated with the ability
        // For example, keep a list of AbilityCards or search in the scene
        return null; // Placeholder
    }
}
