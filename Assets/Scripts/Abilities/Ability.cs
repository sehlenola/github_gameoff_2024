using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public string description;
    public Sprite icon;
    public Sprite targetingIcon;
    public Color32 iconColor;

    public AudioClip abilityAudio;

    public bool supportsPreview = true;
    public bool requiresTarget = true;

    public DiceRequirement diceRequirement;

    // Activate the ability on the target tile using the context
    public abstract void Activate(Tile targetTile, AbilityContext context);

    // Get affected tiles for preview, using the context
    public virtual List<Tile> GetAffectedTiles(Tile targetTile, AbilityContext context)
    {
        // Default implementation: return the target tile
        if (requiresTarget)
        {
            return new List<Tile> { targetTile };
        }
        return null;
    }

    // Check if the ability can be activated on the target tile
    public virtual bool CanActivate(Tile targetTile, AbilityContext context)
    {
        // Default implementation: check if the tile is hidden
        return targetTile.CurrentState == Tile.TileState.Hidden;
    }
}
