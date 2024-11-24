using UnityEngine;
using System.Collections.Generic;

public class Submarine
{
    public int length;
    public List<Tile> occupiedTiles = new List<Tile>();
    public bool isDestroyed { get; private set; }

    public Submarine(int length)
    {
        this.length = length;
    }

    public void AddTile(Tile tile)
    {
        occupiedTiles.Add(tile);
        tile.SetSubmarine(this);
    }

    public void CheckIfDestroyed()
    {
        foreach (Tile tile in occupiedTiles)
        {
            if (tile.CurrentState != Tile.TileState.Destroyed)
            {
                return;
            }
        }
        if (!isDestroyed)
        {
            isDestroyed = true;
            SubmarineManager.Instance.OnSubmarineDestroyed(this);
            UpdateTilesOnDestruction();
        }
    }
    private void UpdateTilesOnDestruction()
    {
        foreach (Tile tile in occupiedTiles)
        {
            tile.UpdateTileAppearance();
        }
    }
}