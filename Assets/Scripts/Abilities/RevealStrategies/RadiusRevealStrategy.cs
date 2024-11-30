using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Reveal Strategies/Radius Reveal Strategy")]
public class RadiusRevealStrategy : RevealStrategyBase
{
    public int radius = 1;

    public override List<Tile> GetTiles(Tile targetTile, AbilityContext context)
    {
        GridSystem gridSystem = GridSystem.Instance;
        List<Tile> tiles = new List<Tile>();

        tiles = gridSystem.GetTilesInRadius(targetTile.GridX, targetTile.GridZ, radius);
        return tiles;
    }
}