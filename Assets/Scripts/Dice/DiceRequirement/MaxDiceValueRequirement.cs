using UnityEngine;

[CreateAssetMenu(menuName = "Dice Requirements/Maximum Value")]
public class MaxDiceValueRequirement : DiceRequirement
{
    public int maxValue;

    public override bool IsSatisfiedBy(int diceValue)
    {
        return diceValue <= maxValue;
    }

    public override string GetDescription()
    {
        return $"{maxValue}-";
    }
}