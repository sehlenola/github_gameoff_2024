using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Reveal One Tile")]
public class RevealOneTileAbility : Ability
{
    public override void Activate(Tile targetTile)
    {
        if (CanActivate(targetTile))
        {
            targetTile.RevealTile();
        }
    }
}
