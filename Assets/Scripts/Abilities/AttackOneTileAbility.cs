using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack One Tile")]
public class AttackOneTileAbility : Ability
{
    public override void Activate(Tile targetTile)
    {
        if (CanActivate(targetTile))
        {
            targetTile.AttackTile();
        }
    }

    public override bool CanActivate(Tile targetTile)
    {
        // Can attack hidden or submarine tiles
        return targetTile.CurrentState == Tile.TileState.Hidden || targetTile.CurrentState == Tile.TileState.Submarine;
    }
}
