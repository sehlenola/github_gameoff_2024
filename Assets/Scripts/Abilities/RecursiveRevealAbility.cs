using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abilities/Recursive Reveal")]
public class RecursiveRevealAbility : Ability
{
    public override void Activate(Tile targetTile, AbilityContext abilityContext)
    {
        if (CanActivate(targetTile, abilityContext))
        {
            RevealRecursive(targetTile);
        }
    }

    private void RevealRecursive(Tile tile)
    {
        if (tile.CurrentState != Tile.TileState.Hidden || tile.HasSubmarine)
            return;

        tile.RevealTile();

        if (tile.NeighborSubmarines == 0)
        {
            GridSystem gridSystem = GridSystem.Instance;

            List<Tile> neighbors = gridSystem.GetAdjacentTiles(tile.GridX, tile.GridZ);

            foreach (Tile neighbor in neighbors)
            {
                RevealRecursive(neighbor);
            }
        }
    }
}
