using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public string description;
    public Sprite icon;

    public bool supportsPreview = true;
    public bool requiresTarget = true;

    public abstract void Activate(Tile targetTile);

    public virtual bool CanActivate(Tile targetTile)
    {
        return targetTile.CurrentState == Tile.TileState.Hidden;
    }
    public virtual List<Tile> GetAffectedTiles(Tile targetTile)
    {
        // Default implementation: return the target tile
        if (requiresTarget)
        {
            return new List<Tile> { targetTile };
        }
        return null;
    }
}
