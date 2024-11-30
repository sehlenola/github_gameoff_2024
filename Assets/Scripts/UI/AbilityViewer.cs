using UnityEngine;
using System.Collections.Generic;

public class AbilityViewer : MonoBehaviour
{
    public GameObject abilityViewerItemPrefab;
    public Transform abilitiesContainer;
    public GameObject RewardContainer;

    private List<AbilityViewerItem> abilityViewerItems = new List<AbilityViewerItem>();

    private bool isRewardMode = false;
    private List<Ability> rewardAbilities;

    private void Start()
    {
        // Subscribe to player's ability changes
        Player.Instance.OnPlayerAbilitiesChanged += UpdateAbilityViewer;

        // Initialize the viewer
        UpdateAbilityViewer();
    }

    public void ShowRewards(List<Ability> abilities)
    {
        isRewardMode = true;
        rewardAbilities = abilities;

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

        List<Ability> abilitiesToShow = isRewardMode ? rewardAbilities : Player.Instance.Abilities;

        // Recreate items based on abilities to show
        foreach (Ability ability in abilitiesToShow)
        {
            GameObject itemObj = Instantiate(abilityViewerItemPrefab, abilitiesContainer);
            AbilityViewerItem item = itemObj.GetComponent<AbilityViewerItem>();
            item.SetAbility(ability);

            if (isRewardMode)
            {
                // Configure item for reward selection
                item.SetAsReward();
            }

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

    // Method called when a reward ability is selected
    public void OnRewardAbilitySelected(Ability ability)
    {
        // Add ability to player's abilities
        Player.Instance.AddAbility(ability);

        // Exit reward mode
        isRewardMode = false;

        // Close reward screen
        RewardContainer.SetActive(false);

        // Proceed to next level
        GameManager.Instance.LoadNextLevel();
    }
}
