using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityCard : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    public Ability ability;
    public Image abilityIcon;
    public TextMeshProUGUI abilityName;
    public TextMeshProUGUI diceRequirementText;
    public TextMeshProUGUI abilityDescriptionText;

    public Transform diceSlot;
    private Dice assignedDice;

    private Button abilityButton;

    [SerializeField] private Image cardBackgroundImage;
    [SerializeField] private Color32 cardBackgroundDefaultColor;
    [SerializeField] private Color32 cardBackgroundHighlightColor;

    private void Start()
    {
        cardBackgroundDefaultColor = cardBackgroundImage.color;
        UpdateUI();
        //abilityButton = GetComponent<Button>();
        //abilityButton.onClick.AddListener(OnAbilityClicked);
    }

    private void UpdateUI()
    {
        abilityIcon.sprite = ability.icon;
        abilityName.text = ability.abilityName;
        //diceRequirementText.text = ability.description;
        abilityDescriptionText.text = ability.description;

        // Additional UI updates, such as displaying dice requirements
        if (ability.diceRequirement != null)
        {
            diceRequirementText.text = $"{ability.diceRequirement.GetDescription()}";
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (AbilityManager.Instance.SelectedAbility != null) { return; }

        Dice dice = eventData.pointerDrag?.GetComponent<Dice>();
        if (dice != null)
        {
            if (CanAcceptDice(dice))
            {
                assignedDice = dice;
                dice.OnAcceptedByAbilityCard(diceSlot);
                AbilityManager.Instance.SelectAbility(ability, dice.Value);
                AbilityManager.Instance.SelectCard(this);
                cardBackgroundImage.color = cardBackgroundHighlightColor;
            }
            else
            {
                Debug.Log("Dice not accepted by AbilityCard (CanAcceptDice returned false).");
                dice.ResetPosition();
            }
        }
        else
        {
            Debug.Log("Dropped object is not a dice.");
        }
    }

    public bool CanAcceptDice(Dice dice)
    {
        if (assignedDice != null)
            return false;

        if (ability.diceRequirement != null)
        {
            return ability.diceRequirement.IsSatisfiedBy(dice.Value);
        }
        return true; // No requirement means any dice is acceptable
    }

    public void AcceptDice(Dice dice)
    {
        assignedDice = dice;
        dice.OnAcceptedByAbilityCard(diceSlot);
    }

    public void RemoveDice()
    {
        if (assignedDice != null)
        {
            DiceManager.Instance.ReturnDiceToPool(assignedDice);
            assignedDice = null;
        }
        cardBackgroundImage.color = cardBackgroundDefaultColor;
    }

    private void OnAbilityClicked()
    {

        if (assignedDice != null)
        {
            Debug.Log(ability);
            AbilityManager.Instance.SelectAbility(ability,assignedDice.Value);
            // Enter targeting mode
        }
        else
        {
            Debug.Log("Assign a dice to use this ability.");
        }
    }

    public void OnAbilityUsed()
    {
        if (assignedDice != null)
        {
            // Consume the dice
            Dice diceToRemove = assignedDice;
            //Destroy(assignedDice.gameObject);
            assignedDice = null;
            DiceManager.Instance.RemoveDice(diceToRemove);
        }
        //RemoveDice();
        cardBackgroundImage.color = cardBackgroundDefaultColor;
    }

    public void OnAbilityCancelled()
    {
        RemoveDice();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Ability area detected");
    }

    public void SetAbility(Ability newAbility)
    {
        ability = newAbility;
        UpdateUI();
    }
}
