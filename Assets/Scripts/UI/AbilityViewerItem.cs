using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityViewerItem : MonoBehaviour
{
    public Image abilityIcon, abilityTargetingIcon;
    public TextMeshProUGUI abilityName;
    public TextMeshProUGUI abilityDescriptionText;
    public TextMeshProUGUI diceRequirementText;
    public Button actionButton; // Could be remove or select button

    private Ability ability;
    private bool isRewardItem = false;

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
        UpdateUI();
    }

    private void UpdateUI()
    {
        abilityIcon.sprite = ability.icon;
        abilityTargetingIcon.sprite = ability.targetingIcon;
        abilityIcon.color = ability.iconColor;
        abilityName.text = ability.abilityName;

        actionButton.onClick.RemoveAllListeners();

        if (isRewardItem)
        {
            actionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
            actionButton.onClick.AddListener(OnSelectButtonClicked);
        }
        else
        {
            actionButton.GetComponentInChildren<TextMeshProUGUI>().text = "-";
            actionButton.onClick.AddListener(OnRemoveButtonClicked);
        }

        abilityDescriptionText.text = ability.description;

        // Additional UI updates, such as displaying dice requirements
        if (ability.diceRequirement != null)
        {
            diceRequirementText.text = $"{ability.diceRequirement.GetDescription()}";
        }
    }

    public void SetAsReward()
    {
        isRewardItem = true;
        UpdateUI();
    }

    private void OnRemoveButtonClicked()
    {
        Player.Instance.RemoveAbility(ability);
    }

    private void OnSelectButtonClicked()
    {
        AbilityViewer abilityViewer = GetComponentInParent<AbilityViewer>();
        abilityViewer.OnRewardAbilitySelected(ability);
    }
}
