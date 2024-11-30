using UnityEngine;

[CreateAssetMenu(menuName = "Dice Requirements/Any Value")]
public class AnyDiceRequirement : DiceRequirement
{
    public int requiredValue;

    public override bool IsSatisfiedBy(int diceValue)
    {
        return true;
    }

    public override string GetDescription()
    {
        return "*";
    }
}