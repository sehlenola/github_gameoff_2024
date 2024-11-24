using UnityEngine;

[CreateAssetMenu(menuName = "Dice Requirements/Minimum Value")]
public class MinDiceValueRequirement : DiceRequirement
{
    public int minValue;

    public override bool IsSatisfiedBy(int diceValue)
    {
        return diceValue >= minValue;
    }

    public override string GetDescription()
    {
        return $"Requires dice value ≥ {minValue}";
    }
}