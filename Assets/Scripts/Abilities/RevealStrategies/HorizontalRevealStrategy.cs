using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Reveal Strategies/Horizontal Reveal Strategy")]
public class HorizontalRevealStrategy : RevealStrategyBase
{
    public int lineLength = 3;

    public override List<Tile> GetTiles(Tile targetTile, AbilityContext context)
    {
        GridSystem gridSystem = GridSystem.Instance;
        List<Tile> tiles = new List<Tile>();

        int x = targetTile.GridX;
        int z = targetTile.GridZ;
        int halfLength = lineLength / 2;

        for (int dx = -halfLength; dx <= halfLength; dx++)
        {
            int nx = x + dx;
            if (nx >= 0 && nx < gridSystem.gridWidth)
            {
                Tile tile = gridSystem.GetTileAt(nx, z);
                if (tile != null)
                {
                    tiles.Add(tile);
                }
            }
        }
        return tiles;
    }
}