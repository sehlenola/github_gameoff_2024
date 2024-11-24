using UnityEngine;

[CreateAssetMenu(menuName = "Dice Requirements/Exact Value")]
public class ExactDiceValueRequirement : DiceRequirement
{
    public int requiredValue;

    public override bool IsSatisfiedBy(int diceValue)
    {
        return diceValue == requiredValue;
    }

    public override string GetDescription()
    {
        return $"Requires dice value = {requiredValue}";
    }
}