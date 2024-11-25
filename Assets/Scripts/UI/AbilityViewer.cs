using UnityEngine;
using System.Collections.Generic;

public class AbilityViewer : MonoBehaviour
{
    public GameObject abilityViewerItemPrefab;
    public Transform abilitiesContainer;

    private List<AbilityViewerItem> abilityViewerItems = new List<AbilityViewerItem>();

    private void Start()
    {
        // Subscribe to player's ability changes
        Player.Instance.OnPlayerAbilitiesChanged += UpdateAbilityViewer;

        // Initialize the viewer
        UpdateAbilityViewer();
    }

    private void UpdateAbilityViewer()
    {
        // Clear existing items
        foreach (Transform child in abilitiesContainer)
        {
            Destroy(child.gameObject);
        }
        abilityViewerItems.Clear();

        // Recreate items based on player's abilities
        foreach (Ability ability in Player.Instance.Abilities)
        {
            GameObject itemObj = Instantiate(abilityViewerItemPrefab, abilitiesContainer);
            AbilityViewerItem item = itemObj.GetComponent<AbilityViewerItem>();
            item.SetAbility(ability);
            abilityViewerItems.Add(item);
        }
    }

    // Method to add a new ability (e.g., from a reward system)
    public void AddNewAbility(Ability newAbility)
    {
        if (Player.Instance.Abilities.Count < 6)
        {
            Player.Instance.AddAbility(newAbility);
        }
        else
        {
            // Logic to swap abilities
            // For example, open a UI to let the player choose which ability to replace
            OpenSwapAbilityUI(newAbility);
        }
    }

    private void OpenSwapAbilityUI(Ability newAbility)
    {
        // Implement UI logic to allow the player to select an ability to swap
        // This could involve enabling UI panels, setting up buttons, etc.
        Debug.Log("Opening swap ability UI.");
    }
}
