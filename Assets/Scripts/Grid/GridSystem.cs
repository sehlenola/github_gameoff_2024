using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public static GridSystem Instance { get; private set; }
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float tileSize = 1.1f;
    public GameObject tilePrefab;
    public int submarineCount = 10;

    public Level levelData;
    private Tile[,] grid;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (levelData == null)
        {
            Debug.LogError("Level data not assigned to GridSystem.");
            return;
        }
        gridWidth = levelData.gridWidth;
        gridHeight = levelData.gridHeight;
        GenerateGrid();
        //PlaceSubmarines();
        //CalculateNeighborNumbers();
    }

    public Tile GetTileAt(int x, int z)
    {
        if (x < 0 || x >= gridWidth || z < 0 || z >= gridHeight)
        {
            return null;
        }
        return grid[x, z];
    }

    public List<Tile> GetTilesInRadius(int centerX, int centerZ, int radius)
    {
        List<Tile> tiles = new List<Tile>();

        for (int x = centerX - radius; x <= centerX + radius; x++)
        {
            for (int z = centerZ - radius; z <= centerZ + radius; z++)
            {
                if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
                {
                    int distance = Mathf.Abs(centerX - x) + Mathf.Abs(centerZ - z);
                    if (distance <= radius)
                    {
                        tiles.Add(grid[x, z]);
                    }
                }
            }
        }
        return tiles;
    }

    public List<Tile> GetAdjacentTiles(int x, int z)
    {
        List<Tile> tiles = new List<Tile>();

        int[] dx = { -1, 0, 1, 0 };
        int[] dz = { 0, -1, 0, 1 };

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int nz = z + dz[i];

            if (nx >= 0 && nx < gridWidth && nz >= 0 && nz < gridHeight)
            {
                tiles.Add(grid[nx, nz]);
            }
        }

        return tiles;
    }


    void GenerateGrid()
    {
        grid = new Tile[gridWidth, gridHeight];

        Vector3 startPos = transform.position;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 position = new Vector3(startPos.x + x * tileSize, startPos.y, startPos.z + z * tileSize);
                GameObject newTileObj = Instantiate(tilePrefab, position, Quaternion.identity);
                newTileObj.name = $"Tile_{x}_{z}";

                Tile tileComponent = newTileObj.GetComponent<Tile>();
                tileComponent.SetCoordinates(x, z);

                grid[x, z] = tileComponent;
            }
        }
        SubmarineManager.Instance.PlaceSubmarines();
    }

    void PlaceSubmarines()
    {
        int placedSubmarines = 0;
        while (placedSubmarines < submarineCount)
        {
            int x = Random.Range(0, gridWidth);
            int y = Random.Range(0, gridHeight);

            if (!grid[x, y].HasSubmarine)
            {
                grid[x, y].HasSubmarine = true;
                placedSubmarines++;
            }
        }
    }

    public void CalculateNeighborNumbers()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (!grid[x, y].HasSubmarine)
                {
                    int submarineNeighbors = CountSubmarineNeighbors(x, y);
                    grid[x, y].NeighborSubmarines = submarineNeighbors;
                }
            }
        }
    }

    private int CountSubmarineNeighbors(int x, int z)
    {
        int count = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            int nx = x + dx;
            if (nx < 0 || nx >= gridWidth) continue;

            for (int dz = -1; dz <= 1; dz++)
            {
                int nz = z + dz;
                if (nz < 0 || nz >= gridHeight) continue;
                if (dx == 0 && dz == 0) continue;

                if (grid[nx, nz].HasSubmarine)
                {
                    count++;
                }
            }
        }
        return count;
    }



}
