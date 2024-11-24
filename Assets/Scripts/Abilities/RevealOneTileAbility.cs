using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Reveal One Tile")]
public class RevealOneTileAbility : Ability
{
    public override void Activate(Tile targetTile, AbilityContext abilityContext)
    {
        if (CanActivate(targetTile, abilityContext))
        {
            targetTile.RevealTile();
        }
    }
}
