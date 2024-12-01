using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Reveal Strategies/X Reveal Strategy")]
public class XRevealStrategy : RevealStrategyBase
{
    public int range = 1; // The range of the X formation from the center tile

    public override List<Tile> GetTiles(Tile targetTile, AbilityContext context)
    {
        GridSystem gridSystem = GridSystem.Instance;
        List<Tile> tiles = new List<Tile>();

        int centerX = targetTile.GridX;
        int centerZ = targetTile.GridZ;

        // Include the center tile
        tiles.Add(targetTile);

        for (int i = 1; i <= range; i++)
        {
            // Diagonal up-right
            AddTileIfValid(centerX + i, centerZ + i, tiles, gridSystem);

            // Diagonal up-left
            AddTileIfValid(centerX - i, centerZ + i, tiles, gridSystem);

            // Diagonal down-right
            AddTileIfValid(centerX + i, centerZ - i, tiles, gridSystem);

            // Diagonal down-left
            AddTileIfValid(centerX - i, centerZ - i, tiles, gridSystem);
        }

        return tiles;
    }

    private void AddTileIfValid(int x, int z, List<Tile> tiles, GridSystem gridSystem)
    {
        if (x >= 0 && x < gridSystem.gridWidth && z >= 0 && z < gridSystem.gridHeight)
        {
            Tile tile = gridSystem.GetTileAt(x, z);
            if (tile != null)
            {
                tiles.Add(tile);
            }
        }
    }
}