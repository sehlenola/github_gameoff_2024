using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileState { Hidden, Revealed, Submarine, Destroyed }
    public TileState CurrentState = TileState.Hidden;

    public int NeighborSubmarines = 0;
    public bool HasSubmarine = false;

    public int GridX { get; private set; }
    public int GridZ { get; private set; }

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        UpdateTileAppearance();
    }

    private void OnMouseDown()
    {
        RevealTile();
    }
    public void SetCoordinates(int x, int z)
    {
        GridX = x;
        GridZ = z;
    }
    public void RevealTile()
    {
        if (CurrentState == TileState.Hidden)
        {
            if (HasSubmarine)
            {
                CurrentState = TileState.Submarine;
            }
            else
            {
                CurrentState = TileState.Revealed;
                if (NeighborSubmarines == 0)
                {
                    RevealAdjacentTiles();
                }
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

        switch (CurrentState)
        {
            case TileState.Hidden:
                propertyBlock.SetColor("_BaseColor", Color.white);
                text.text = "";
                break;
            case TileState.Revealed:
                Color color = NeighborSubmarines > 0 ? new Color(1f, 0.65f, 0f) : Color.gray; // Orange or Gray
                propertyBlock.SetColor("_BaseColor", color);
                text.text = NeighborSubmarines > 0 ? NeighborSubmarines.ToString() : "";
                break;
            case TileState.Submarine:
                propertyBlock.SetColor("_BaseColor", Color.red);
                text.text = "!";
                break;
            case TileState.Destroyed:
                propertyBlock.SetColor("_BaseColor", Color.green);
                text.text = "X";
                break;
        }

        meshRenderer.SetPropertyBlock(propertyBlock);
    }
}
