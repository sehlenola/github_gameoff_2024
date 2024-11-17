using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }

    public Ability SelectedAbility;
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
        SelectedAbility = ability;
        // Update UI to reflect selected ability
    }

    public void UseAbilityOnTile(Tile targetTile)
    {
        if (SelectedAbility != null)
        {
            if (SelectedAbility.CanActivate(targetTile))
            {
                SelectedAbility.Activate(targetTile);
                // Handle cooldowns, turn limits, or ability consumption here
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
}
