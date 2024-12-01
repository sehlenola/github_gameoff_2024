using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SubmarineStatusUI : MonoBehaviour
{
    public static SubmarineStatusUI Instance { get; private set; }

    public GameObject statusItemPrefab;
    public Transform statusContainer;

    private Dictionary<int, SubmarineStatusItem> statusItems = new Dictionary<int, SubmarineStatusItem>();

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUI()
    {
        foreach (Transform child in statusContainer)
        {
            Destroy(child.gameObject);
        }
        statusItems.Clear();

        Level levelData = GameManager.Instance.currentLevel;
        List<Submarine> submarines = SubmarineManager.Instance.GetSubmarines();

        Dictionary<int, int> totalCounts = new Dictionary<int, int>();
        Dictionary<int, int> destroyedCounts = new Dictionary<int, int>();
        Dictionary<int, Sprite> submarineSprites = new Dictionary<int, Sprite>();

        foreach (Level.SubmarineConfig config in levelData.submarines)
        {
            totalCounts[config.length] = config.count;
            destroyedCounts[config.length] = 0;
            submarineSprites[config.length] = config.sprite;
        }

        foreach (Submarine submarine in submarines)
        {
            if (submarine.isDestroyed)
            {
                if (destroyedCounts.ContainsKey(submarine.length))
                {
                    destroyedCounts[submarine.length]++;
                }
                else
                {
                    destroyedCounts[submarine.length] = 1;
                }
            }
        }

        foreach (int length in totalCounts.Keys)
        {
            GameObject itemObj = Instantiate(statusItemPrefab, statusContainer);
            SubmarineStatusItem item = itemObj.GetComponent<SubmarineStatusItem>();
            item.SetStatus(length, destroyedCounts[length], totalCounts[length], submarineSprites[length]);
            statusItems[length] = item;
        }
    }
}
