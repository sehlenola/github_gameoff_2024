using UnityEngine;

[CreateAssetMenu(menuName = "Dice Requirements/Range Value")]
public class RangeDiceRequirement : DiceRequirement
{
    public int minValue;
    public int maxValue;

    public override bool IsSatisfiedBy(int diceValue)
    {
        return diceValue >= minValue && diceValue <= maxValue;
    }

    public override string GetDescription()
    {
        return $"{minValue}-{maxValue}";
    }
}