using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityViewerItem : MonoBehaviour
{
    public Image abilityIcon;
    public TextMeshProUGUI abilityName;
    public Button removeButton;
    public Button addButton;

    private Ability ability;

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
        UpdateUI();
    }

    private void UpdateUI()
    {
        abilityIcon.sprite = ability.icon;
        abilityName.text = ability.abilityName;

        removeButton.onClick.RemoveAllListeners();
        removeButton.onClick.AddListener(OnRemoveButtonClicked);

        addButton.onClick.RemoveAllListeners();
        addButton.onClick.AddListener(OnAddButtonClicked);
    }

    private void OnRemoveButtonClicked()
    {
        Player.Instance.RemoveAbility(ability);
    }

    private void OnAddButtonClicked()
    {
        Player.Instance.AddAbility(ability);
    }

    // Optional: Implement swapping logic if needed
    public void OnSwapButtonClicked(Ability newAbility)
    {
        Player.Instance.SwapAbility(ability, newAbility);
    }
}
