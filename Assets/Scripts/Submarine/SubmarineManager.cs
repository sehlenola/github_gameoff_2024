using UnityEngine;
using System.Collections.Generic;

public class SubmarineManager : MonoBehaviour
{
    public static SubmarineManager Instance { get; private set; }

    public Level levelData;

    [System.Serializable]
    public class SubmarineModelEntry
    {
        public int length;
        public GameObject modelPrefab;
    }
    public List<SubmarineModelEntry> submarineModels = new List<SubmarineModelEntry>();
    private Dictionary<int, GameObject> submarineModelPrefabs = new Dictionary<int, GameObject>();
    private List<GameObject> submarineModelInstances = new List<GameObject>();


    private List<Submarine> submarines = new List<Submarine>();
    private GridSystem gridSystem;

    private void Awake()
    {
        Instance = this;
        // Initialize the dictionary for quick lookup
        foreach (var entry in submarineModels)
        {
            if (!submarineModelPrefabs.ContainsKey(entry.length))
            {
                submarineModelPrefabs.Add(entry.length, entry.modelPrefab);
            }
        }
    }

    private void Start()
    {
        gridSystem = GridSystem.Instance;

        if (levelData == null)
        {
            Debug.LogError("Level data not assigned to SubmarineManager.");
            return;
        }
    }

    public void PlaceSubmarines()
    {
        foreach (Level.SubmarineConfig config in levelData.submarines)
        {
            for (int i = 0; i < config.count; i++)
            {
                bool placed = PlaceSubmarine(config.length);
                if (!placed)
                {
                    Debug.LogError($"Failed to place submarine of length {config.length}");
                }
            }
        }
        GridSystem.Instance.CalculateNeighborNumbers();
        UpdateSubmarineUI();
    }

    private bool PlaceSubmarine(int length)
    {
        int maxAttempts = 100;
        int attempts = 0;
        while (attempts < maxAttempts)
        {
            attempts++;
            bool horizontal = Random.Range(0, 2) == 0;
            int x = Random.Range(0, gridSystem.gridWidth - (horizontal ? length : 1));
            int z = Random.Range(0, gridSystem.gridHeight - (horizontal ? 1 : length));

            if (CanPlaceSubmarineAt(x, z, length, horizontal))
            {
                Submarine submarine = new Submarine(length);
                for (int i = 0; i < length; i++)
                {
                    int nx = horizontal ? x + i : x;
                    int nz = horizontal ? z : z + i;
                    Tile tile = gridSystem.GetTileAt(nx, nz);
                    submarine.AddTile(tile);
                }
                submarines.Add(submarine);
                return true;
            }
        }
        return false;
    }

    private bool CanPlaceSubmarineAt(int x, int z, int length, bool horizontal)
    {
        for (int i = 0; i < length; i++)
        {
            int nx = horizontal ? x + i : x;
            int nz = horizontal ? z : z + i;

            if (nx < 0 || nx >= gridSystem.gridWidth || nz < 0 || nz >= gridSystem.gridHeight)
            {
                return false;
            }

            Tile tile = gridSystem.GetTileAt(nx, nz);
            if (tile.CurrentState == Tile.TileState.Submarine)
            {
                return false;
            }
        }
        return true;
    }

    public void OnSubmarineDestroyed(Submarine submarine)
    {
        UpdateSubmarineUI();
        SpawnSubmarineModel(submarine);
        // Additional logic for when a submarine is destroyed
        Debug.Log($"Submarine of length {submarine.length} destroyed!");
    }

    private void UpdateSubmarineUI()
    {
        // Implement UI update logic here
        SubmarineStatusUI.Instance.UpdateUI();
    }

    public List<Submarine> GetSubmarines()
    {
        return submarines;
    }

    private void SpawnSubmarineModel(Submarine submarine)
    {
        GameObject modelPrefab = GetSubmarineModelPrefab(submarine.length);
        if (modelPrefab == null)
        {
            Debug.LogError($"No submarine model prefab assigned for length {submarine.length}");
            return;
        }

        // Calculate position and rotation
        Vector3 position;
        Quaternion rotation;
        CalculateSubmarineTransform(submarine, out position, out rotation);

        // Instantiate the model
        GameObject modelInstance = Instantiate(modelPrefab, position, rotation);
        submarineModelInstances.Add(modelInstance);
    }

    private void CalculateSubmarineTransform(Submarine submarine, out Vector3 position, out Quaternion rotation)
    {
        List<Tile> tiles = submarine.occupiedTiles;

        // Get positions of the start and end tiles
        Vector3 startPos = tiles[0].transform.position;
        Vector3 endPos = tiles[tiles.Count - 1].transform.position;

        // Calculate center position
        position = (startPos + endPos) / 2;

        if (tiles.Count == 1)
        {
            // For single-tile submarines, default to vertical orientation
            rotation = Quaternion.identity;
        }
        else
        {
            // Determine the orientation based on the position of the first two tiles
            bool isHorizontal = tiles[0].GridZ == tiles[1].GridZ;

            if (isHorizontal)
            {
                rotation = Quaternion.Euler(0, 90, 0); // Facing along the X-axis
            }
            else
            {
                rotation = Quaternion.identity; // Facing along the Z-axis
            }
        }
    }
    private GameObject GetSubmarineModelPrefab(int length)
    {
        submarineModelPrefabs.TryGetValue(length, out GameObject prefab);
        return prefab;
    }
}
