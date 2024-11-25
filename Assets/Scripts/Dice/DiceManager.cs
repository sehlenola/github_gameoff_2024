using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public DiceArea diceArea;

    public int numberOfDice = 3;

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

    private void Start()
    {
        RollDice();
    }

    public void RollDice()
    {
        ClearDice();

        for (int i = 0; i < numberOfDice; i++)
        {
            GameObject diceObj = Instantiate(diceArea.dicePrefab, diceArea.diceContainer);
            Dice dice = diceObj.GetComponent<Dice>();
            dice.Roll();
            dicePool.Add(dice);
        }
    }

    public void RerollUnusedDice()
    {
        foreach (Dice dice in dicePool)
        {
            if (dice.transform.parent == diceArea.diceContainer)
            {
                dice.Roll();
            }
        }
    }

    public void ClearDice()
    {
        diceArea.ClearDice();
        dicePool.Clear();
    }

    public void ReturnDiceToPool(Dice dice)
    {
        diceArea.AddDice(dice);
    }
    public void RemoveDice(Dice dice)
    {
        dicePool.Remove(dice);
        diceArea.DestroyDice(dice);
    }
}
