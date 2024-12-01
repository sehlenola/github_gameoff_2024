using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public DiceArea diceArea;

    public int numberOfDice = 3;
    public int maxRerolls = 3;
    public int currentRerolls = 3;

    [SerializeField] private TextMeshProUGUI rerollButtonText;
    [SerializeField] private TextMeshProUGUI diceCounterText;
    [SerializeField] private AudioClip rollDiceClip, rollSingeDiceClip;

    private List<Dice> dicePool = new List<Dice>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void UpdateDiceText()
    {
        diceCounterText.text = "Dice: " + dicePool.Count.ToString() + "/" + GameManager.Instance.currentLevel.maxDice.ToString();
    }
    public void ResetRerolls()
    {
        currentRerolls = maxRerolls;
    }

    private void Start()
    {
        //RollDice();
    }
    public void RollAllDice()
    {
        RollDice();
    }
    public void RollDice()
    {
        ClearDice();
        AudioManager.Instance.PlaySound(rollDiceClip);
        for (int i = 0; i < GameManager.Instance.currentLevel.maxDice; i++)
        {
            AddSingleDice();
        }
        rerollButtonText.text = "Reroll - " + Player.Instance.CurrentRerolls;
        UpdateDiceText();
    }

    public void AddSingleDice(int min = 1, int max = 6)
    {
        GameObject diceObj = Instantiate(diceArea.dicePrefab, diceArea.diceContainer);
        Dice dice = diceObj.GetComponent<Dice>();
        dice.Roll(min, max);
        dicePool.Add(dice);
    }


    public void RerollUnusedDice()
    {
        if (Player.Instance.CurrentRerolls > 0)
        {
            foreach (Dice dice in dicePool)
            {
                if (dice.transform.parent == diceArea.diceContainer)
                {
                    dice.Roll();
                }
            }

            Player.Instance.CurrentRerolls--;
            rerollButtonText.text = "Reroll - " + Player.Instance.CurrentRerolls;
            AudioManager.Instance.PlaySound(rollSingeDiceClip);
        }
        else
        {
            AudioManager.Instance.PlayErrorSound();
            return;
        }
    }

    public void ClearDice()
    {
        diceArea.ClearDice();
        dicePool.Clear();
        UpdateDiceText();
    }

    public void ReturnDiceToPool(Dice dice)
    {
        AudioManager.Instance.PlayErrorSound();
        diceArea.AddDice(dice);
        UpdateDiceText();
    }
    public void RemoveDice(Dice dice)
    {
        dicePool.Remove(dice);
        diceArea.DestroyDice(dice);
        UpdateDiceText();
        StaticEventHandler.CallOnDiceUsed();
    }
    public int GetDiceCount()
    {
        return dicePool.Count;
    }
}
