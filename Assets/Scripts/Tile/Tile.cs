using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileState { Hidden, Revealed, Submarine, Destroyed }
    public TileState CurrentState = TileState.Hidden;

    public int NeighborSubmarines = 0;
    public bool HasSubmarine = false;

    private bool isHighlighted = false;

    public int GridX { get; private set; }
    public int GridZ { get; private set; }

    public bool IsRevealed { get; private set; }
    public Submarine submarine;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        UpdateTileAppearance();
    }

    public void SetHighlighted(bool highlighted)
    {
        isHighlighted = highlighted;
        UpdateTileAppearance();
    }
    private void OnMouseEnter()
    {
        if (AbilityManager.Instance.SelectedAbility != null)
        {
            Ability ability = AbilityManager.Instance.SelectedAbility;

            if (ability.supportsPreview)
            {
                AbilityContext context = AbilityManager.Instance.AbilityContext;
                List<Tile> tilesToHighlight = ability.GetAffectedTiles(this, context);

                AbilityManager.Instance.HighlightTiles(tilesToHighlight);
            }
        }
    }
    public void SetSubmarine(Submarine submarine)
    {
        this.submarine = submarine;
        HasSubmarine = true;
    }
    private void OnMouseExit()
    {
        if (AbilityManager.Instance.SelectedAbility != null)
        {
            AbilityManager.Instance.ClearHighlightedTiles();
        }
    }
    private void OnMouseDown()
    {
        if (AbilityManager.Instance.SelectedAbility != null)
        {
            AbilityManager.Instance.UseAbilityOnTile(this);
        }
        else
        {
            Debug.Log("No ability selected.");
        }
    }
    public void SetCoordinates(int x, int z)
    {
        GridX = x;
        GridZ = z;
    }

    public void AttackTile()
    {
        if (CurrentState == TileState.Hidden || CurrentState == TileState.Submarine)
        {
            if (submarine != null)
            {
                CurrentState = TileState.Destroyed;
                UpdateTileAppearance();
                submarine.CheckIfDestroyed();
            }
            else
            {
                CurrentState = TileState.Revealed;
                UpdateTileAppearance();
            }
        }
    }
    public void RevealTile()
    {
        
        if (CurrentState == TileState.Hidden)
        {
            if (HasSubmarine)
            {
                CurrentState = TileState.Submarine;
                if (submarine != null)
                {
                    submarine.CheckIfDestroyed();
                }
            }
            else
            {
                CurrentState = TileState.Revealed;

            }
            UpdateTileAppearance();
        }
    }

    void RevealAdjacentTiles()
    {
        int x = GridX;
        int z = GridZ;

        for (int dx = -1; dx <= 1; dx++)
        {
            int nx = x + dx;
            if (nx < 0 || nx >= GridSystem.Instance.gridWidth) continue;

            for (int dz = -1; dz <= 1; dz++)
            {
                int nz = z + dz;
                if (nz < 0 || nz >= GridSystem.Instance.gridHeight) continue;
                if (dx == 0 && dz == 0) continue;

                Tile neighborTile = GridSystem.Instance.GetTileAt(nx, nz);
                if (neighborTile != null && neighborTile.CurrentState == TileState.Hidden)
                {
                    neighborTile.RevealTile();
                }
            }
        }
    }

    public void UpdateTileAppearance()
    {
        
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(propertyBlock);

        Color baseColor;

        switch (CurrentState)
        {
            case TileState.Hidden:
                baseColor = Color.white;
                text.text = "";
                break;
            case TileState.Revealed:
                baseColor = NeighborSubmarines > 0 ? new Color(1f, 0.65f, 0f) : Color.gray; // Orange or Gray
                text.text = NeighborSubmarines > 0 ? NeighborSubmarines.ToString() : "";
                break;
            case TileState.Submarine:
                baseColor = Color.red;
                text.text = "!";
                break;
            case TileState.Destroyed:
                if (submarine != null && submarine.isDestroyed)
                {
                    baseColor = Color.black;
                    text.text = "X";
                }
                else
                {
                    baseColor = Color.green;
                    text.text = "Hit!";
                }
                break;
            default:
                baseColor = Color.white;
                text.text = "";
                break;
        }

        if (isHighlighted)
        {
            // Blend the base color with yellow to indicate highlighting
            baseColor = Color.Lerp(baseColor, Color.yellow, 0.5f);
        }

        propertyBlock.SetColor("_BaseColor", baseColor);
        meshRenderer.SetPropertyBlock(propertyBlock);
    }
        


}
