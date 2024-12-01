using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abilities/X Reveal")]
public class RevealXFormationAbility : Ability
{
    public RevealStrategyBase revealStrategy;

    public override void Activate(Tile targetTile, AbilityContext context)
    {
        List<Tile> tilesToReveal = GetAffectedTiles(targetTile, context);

        foreach (Tile tile in tilesToReveal)
        {
            if (CanActivate(tile, context))
            {
                tile.RevealTile();
            }
        }
    }

    public override List<Tile> GetAffectedTiles(Tile targetTile, AbilityContext context)
    {
        return revealStrategy.GetTiles(targetTile, context);
    }
}