using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityCard : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    public Ability ability;
    public Image abilityIcon;
    public TextMeshProUGUI abilityName;
    public TextMeshProUGUI abilityDescription;

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
        abilityDescription.text = ability.description;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop called on AbilityCard.");

        Dice dice = eventData.pointerDrag?.GetComponent<Dice>();
        if (dice != null)
        {
            if (CanAcceptDice(dice))
            {
                assignedDice = dice;
                dice.OnAcceptedByAbilityCard(diceSlot);
                AbilityManager.Instance.SelectAbility(ability);
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
        // Implement logic to check if the dice value meets the ability's requirements
        // For now, we'll assume any dice can be accepted
        return assignedDice == null;
    }

    public void AcceptDice(Dice dice)
    {
        assignedDice = dice;
        //dice.transform.SetParent(diceSlot);
        //dice.transform.localPosition = Vector3.zero;
        //dice.transform.localScale = Vector3.one;
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
            AbilityManager.Instance.SelectAbility(ability);
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
            Destroy(assignedDice.gameObject);
            assignedDice = null;
        }
        RemoveDice();
    }

    public void OnAbilityCancelled()
    {
        RemoveDice();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Ability area detected");
    }
}
