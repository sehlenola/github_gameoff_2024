using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Level")]
public class Level : ScriptableObject
{
    public int gridWidth = 10;
    public int gridHeight = 10;

    [System.Serializable]
    public class SubmarineConfig
    {
        public int length;
        public int count;
        public Sprite sprite;
    }

    public List<SubmarineConfig> submarines = new List<SubmarineConfig>();

    // New property for max turns
    public int maxTurns = 3;
    public int maxDice = 3;
}
