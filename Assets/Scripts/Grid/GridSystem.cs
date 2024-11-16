using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public static GridSystem Instance { get; private set; }
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float tileSize = 1.1f;
    public GameObject tilePrefab;
    public int submarineCount = 10;

    private Tile[,] grid;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GenerateGrid();
        PlaceSubmarines();
        CalculateNeighborNumbers();
    }

    public Tile GetTileAt(int x, int z)
    {
        if (x < 0 || x >= gridWidth || z < 0 || z >= gridHeight)
        {
            return null;
        }
        return grid[x, z];
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

    void CalculateNeighborNumbers()
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

    int CountSubmarineNeighbors(int x, int z)
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
