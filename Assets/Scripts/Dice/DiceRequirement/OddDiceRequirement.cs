using UnityEngine;

[CreateAssetMenu(menuName = "Dice Requirements/Odd Value")]
public class OddDiceValueRequirement : DiceRequirement
{
    public override bool IsSatisfiedBy(int diceValue)
    {
        return diceValue % 2 != 0;
    }

    public override string GetDescription()
    {
        return "Odd";
    }
}