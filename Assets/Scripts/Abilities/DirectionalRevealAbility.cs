using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abilities/Directional Reveal")]
public class DirectionalRevealAbility : Ability
{
    public RevealStrategyBase evenStrategy;
    public RevealStrategyBase oddStrategy;

    public override void Activate(Tile targetTile, AbilityContext context)
    {
        List<Tile> tilesToReveal = GetAffectedTiles(targetTile, context);

        foreach (Tile tile in tilesToReveal)
        {
            if (CanActivate(tile, context))
            {
                tile.RevealTile();
            }
        }
    }

    public override List<Tile> GetAffectedTiles(Tile targetTile, AbilityContext context)
    {
        RevealStrategyBase strategy = GetStrategyBasedOnDiceValue(context.diceValue);
        return strategy.GetTiles(targetTile, context);
    }

    private RevealStrategyBase GetStrategyBasedOnDiceValue(int diceValue)
    {
        if (diceValue % 2 == 0)
        {
            // Even dice value: use evenStrategy
            return evenStrategy;
        }
        else
        {
            // Odd dice value: use oddStrategy
            return oddStrategy;
        }
    }
}