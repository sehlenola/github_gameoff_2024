using UnityEngine;

[CreateAssetMenu(menuName = "Dice Requirements/Even Value")]
public class EvenDiceValueRequirement : DiceRequirement
{
    public override bool IsSatisfiedBy(int diceValue)
    {
        return diceValue % 2 == 0;
    }

    public override string GetDescription()
    {
        return "Even";
    }
}