using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack In X Formation Ability")]
public class AttackInXFormationAbility : Ability
{
    public RevealStrategyBase attackStrategy;

    public override void Activate(Tile targetTile, AbilityContext context)
    {
        if (targetTile == null)
        {
            Debug.LogError("Target tile is null.");
            return;
        }

        if (attackStrategy == null)
        {
            Debug.LogError("Attack strategy is not assigned.");
            return;
        }

        List<Tile> tilesToAttack = attackStrategy.GetTiles(targetTile, context);

        foreach (Tile tile in tilesToAttack)
        {
            tile.AttackTile();
        }

    }

    public override bool CanActivate(Tile targetTile, AbilityContext abilityContext)
    {
        // Can attack hidden or submarine tiles
        return targetTile.CurrentState == Tile.TileState.Hidden || targetTile.CurrentState == Tile.TileState.Submarine;
    }
    public override List<Tile> GetAffectedTiles(Tile targetTile, AbilityContext context)
    {
        return attackStrategy.GetTiles(targetTile, context);
    }
}
