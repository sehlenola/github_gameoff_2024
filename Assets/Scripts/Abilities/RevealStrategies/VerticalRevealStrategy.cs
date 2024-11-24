using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Reveal Strategies/Vertical Reveal Strategy")]
public class VerticalRevealStrategy : RevealStrategyBase
{
    public int lineLength = 3;

    public override List<Tile> GetTiles(Tile targetTile, AbilityContext context)
    {
        GridSystem gridSystem = GridSystem.Instance;
        List<Tile> tiles = new List<Tile>();

        int x = targetTile.GridX;
        int z = targetTile.GridZ;
        int halfLength = lineLength / 2;

        for (int dz = -halfLength; dz <= halfLength; dz++)
        {
            int nz = z + dz;
            if (nz >= 0 && nz < gridSystem.gridHeight)
            {
                Tile tile = gridSystem.GetTileAt(x, nz);
                if (tile != null)
                {
                    tiles.Add(tile);
                }
            }
        }
        return tiles;
    }
}