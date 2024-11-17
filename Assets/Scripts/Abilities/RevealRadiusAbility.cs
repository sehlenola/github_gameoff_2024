using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Reveal Radius")]
public class RevealRadiusAbility : Ability
{
    public int radius = 1;

    public override void Activate(Tile targetTile)
    {
        GridSystem gridSystem = GridSystem.Instance;

        List<Tile> tilesInRadius = gridSystem.GetTilesInRadius(targetTile.GridX, targetTile.GridZ, radius);

        foreach (Tile tile in tilesInRadius)
        {
            if (CanActivate(tile))
            {
                tile.RevealTile();
            }
        }
    }
    public override List<Tile> GetAffectedTiles(Tile targetTile)
    {
        GridSystem gridSystem = GridSystem.Instance;
        return gridSystem.GetTilesInRadius(targetTile.GridX, targetTile.GridZ, radius);
    }
}
