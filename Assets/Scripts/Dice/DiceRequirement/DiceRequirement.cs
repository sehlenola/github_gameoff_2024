using UnityEngine;

public abstract class DiceRequirement : ScriptableObject
{
    public abstract bool IsSatisfiedBy(int diceValue);
    public abstract string GetDescription();
}