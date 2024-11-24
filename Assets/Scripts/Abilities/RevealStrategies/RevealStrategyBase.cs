using UnityEngine;
using System.Collections.Generic;

public abstract class RevealStrategyBase : ScriptableObject
{
    public abstract List<Tile> GetTiles(Tile targetTile, AbilityContext context);
}