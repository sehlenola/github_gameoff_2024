using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack Row")]
public class AttackRowAbility : Ability
{
    public int radius = 1;
    public RevealStrategyBase RevealStrategy;

    public override void Activate(Tile targetTile, AbilityContext abilityContext)
    {
        GridSystem gridSystem = GridSystem.Instance;

        List<Tile> tilesInRadius = RevealStrategy.GetTiles(targetTile, abilityContext);

        foreach (Tile tile in tilesInRadius)
        {
            if (CanActivate(tile, abilityContext))
            {
                tile.AttackTile();
            }
        }
    }
    public override List<Tile> GetAffectedTiles(Tile targetTile, AbilityContext abilityContext)
    {
        return RevealStrategy.GetTiles(targetTile, abilityContext);
    }

    public override bool CanActivate(Tile targetTile, AbilityContext abilityContext)
    {
        // Can attack hidden or submarine tiles
        return targetTile.CurrentState == Tile.TileState.Hidden || targetTile.CurrentState == Tile.TileState.Submarine;
    }
}
