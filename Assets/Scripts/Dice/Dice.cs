using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dice : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int Value { get; private set; }

    private bool droppedOnValidTarget = false;
    private Transform originalParent;
    private Vector3 originalPosition;

    [SerializeField] private Image diceImage;
    [SerializeField] private TextMeshProUGUI valueText;

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void Roll(int minValue = 1, int maxValue = 6)
    {
        Value = Random.Range(minValue, maxValue + 1);
        UpdateAppearance();
    }

    public void SetValue(int value)
    {
        Value = value;
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        valueText.text = Value.ToString();
        // Optionally, update diceImage sprite based on Value
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        diceImage.raycastTarget = false;
        valueText.raycastTarget = false;
        droppedOnValidTarget = false;
        originalParent = transform.parent;
        originalPosition = transform.position;
        transform.SetParent(canvas.transform); // Move to root canvas to not be clipped

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        diceImage.raycastTarget = true;
        valueText.raycastTarget = true;
        // Check if dropped over a valid drop zone (AbilityCard)
        Debug.Log(eventData.pointerEnter);
        if (eventData.pointerEnter != null)
        {
            AbilityCard abilityCard = eventData.pointerEnter.GetComponentInParent<AbilityCard>();
            if (abilityCard != null)
            {
                if (abilityCard.CanAcceptDice(this))
                {
                    abilityCard.AcceptDice(this);
                    return;
                }
            }
        }
        if (!droppedOnValidTarget)
        {
            // Return to original position if not dropped on a valid target
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        Debug.Log("Resetting position lol");
        transform.SetParent(originalParent);
        transform.position = originalPosition;
    }

    // New method to be called when accepted by an AbilityCard
    public void OnAcceptedByAbilityCard(Transform newParent)
    {
        droppedOnValidTarget = true;
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}

