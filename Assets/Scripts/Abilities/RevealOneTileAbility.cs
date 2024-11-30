using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Reveal One Tile")]
public class RevealOneTileAbility : Ability
{
    public override void Activate(Tile targetTile, AbilityContext abilityContext)
    {
        if (CanActivate(targetTile, abilityContext))
        {
            targetTile.RevealTile();
            if (targetTile.submarine == null && abilityContext.diceValue > 1)
            {
                DiceManager.Instance.AddSingleDice(1, abilityContext.diceValue - 1);
            }
        }
    }
}
