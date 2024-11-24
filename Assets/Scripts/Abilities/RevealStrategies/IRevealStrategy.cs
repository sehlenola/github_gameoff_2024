using System.Collections.Generic;

public interface IRevealStrategy
{
    List<Tile> GetTiles(Tile targetTile, AbilityContext context);
}