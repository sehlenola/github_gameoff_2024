using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // List of levels
    public List<Level> levels = new List<Level>();

    // Current level index
    private int currentLevelIndex = 0;

    // Current level data
    public Level currentLevel;

    // Turn management
    public int maxTurns;
    private int currentTurn;

    public AbilityPool abilityPool;

    // Reference to other managers
    private GridSystem gridSystem;
    private SubmarineManager submarineManager;
    private DiceManager diceManager;
    private Player player;

    // Game state
    private bool isLevelComplete = false;
    private bool isGameOver = false;

    // UI Elements
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [SerializeField] private TextMeshProUGUI levelText;
    public GameObject rewardScreen; // UI panel for rewards
    public AbilityViewer abilityViewer; // Reference to AbilityViewer for rewards

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional, if you want the GameManager to persist
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize references
        gridSystem = GridSystem.Instance;
        submarineManager = SubmarineManager.Instance;
        diceManager = DiceManager.Instance;
        player = Player.Instance;

        // Start the first level
        StartLevel(currentLevelIndex);
    }

    // Start a level based on the index
    public void StartLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count)
        {
            Debug.LogError("Invalid level index.");
            return;
        }

        currentLevelIndex = levelIndex;
        currentLevel = levels[currentLevelIndex];
        levelText.text = "Level: " + (currentLevelIndex +1).ToString();

        // Load level data into GridSystem and SubmarineManager
        gridSystem.SetupGrid(currentLevel);
        submarineManager.SetupSubmarines(currentLevel);
        gridSystem.CalculateNeighborNumbers();

        // Set up turn management
        maxTurns = currentLevel.maxTurns;
        currentTurn = 0;

        // Start the first turn
        StartTurn();
    }

    // Start a new turn
    public void StartTurn()
    {
        if (isLevelComplete || isGameOver)
            return;

        if (currentTurn >= maxTurns)
        {
            // No more turns left
            CheckGameOver();
            return;
        }

        currentTurn++;

        // Reset player's rerolls
        player.ResetRerolls();

        // Roll all dice
        diceManager.RollAllDice();
        UpdateTurnUI();


    }

    // Call this method when the player ends their turn
    public void EndTurn()
    {
        // Check win condition
        if (submarineManager.AreAllSubmarinesDestroyed())
        {
            LevelComplete();
        }
        else
        {
            StartTurn();
        }
    }

    // Called when the level is completed
    private void LevelComplete()
    {
        isLevelComplete = true;
        Debug.Log("Level Complete!");

        // Show reward screen
        ShowRewardScreen();
    }

    // Check if the game is over (no more turns)
    private void CheckGameOver()
    {
        if (!submarineManager.AreAllSubmarinesDestroyed())
        {
            isGameOver = true;
            Debug.Log("Game Over! You have run out of turns.");

            // Handle game over (show game over screen, etc.)
            ShowGameOverScreen();
        }
        else
        {
            LevelComplete();
        }
    }

    // Proceed to the next level
    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levels.Count)
        {
            // No more levels, player wins the game
            GameWon();
        }
        else
        {
            // Reset game state
            isLevelComplete = false;
            isGameOver = false;

            // Remove current grid and submarines
            gridSystem.ClearGrid();
            submarineManager.ClearSubmarines();

            // Start the next level
            StartLevel(currentLevelIndex);
        }
    }

    // Called when the player has completed all levels
    private void GameWon()
    {
        Debug.Log("Congratulations! You have won the game!");
        // Implement game win logic (show win screen, etc.)
        ShowGameWonScreen();
    }

    private void ShowRewardScreen()
    {
        if (rewardScreen != null)
        {
            rewardScreen.SetActive(true);

            // Assuming abilityViewer is part of the reward screen
            if (abilityViewer != null)
            {
                // Present new abilities as rewards
                List<Ability> rewardAbilities = GenerateRewardAbilities();
                abilityViewer.ShowRewards(rewardAbilities);
            }
            else
            {
                Debug.LogError("AbilityViewer is not assigned.");
            }
        }
        else
        {
            Debug.LogError("Reward screen is not assigned.");
        }
    }

    private List<Ability> GenerateRewardAbilities()
    {
        if (abilityPool == null)
        {
            Debug.LogError("AbilityPool is not assigned in GameManager.");
            return new List<Ability>();
        }

        // Exclude abilities the player already has
        List<Ability> playerAbilities = Player.Instance.Abilities;

        // Get 3 random abilities from the pool, excluding the player's current abilities
        List<Ability> rewardAbilities = abilityPool.GetRandomAbilities(2, playerAbilities);

        return rewardAbilities;
    }

    // Show game over screen
    private void ShowGameOverScreen()
    {
        // Implement game over UI logic
        Debug.Log("Displaying Game Over Screen.");
    }

    // Show game won screen
    private void ShowGameWonScreen()
    {
        // Implement game won UI logic
        Debug.Log("Displaying Game Won Screen.");
    }

    // Update the turn UI
    private void UpdateTurnUI()
    {
        gameStatusText.text = $"Turn {currentTurn}/{maxTurns}";
    }

    // Method called when a submarine is destroyed to check win condition
    public void OnSubmarineDestroyed()
    {
        if (submarineManager.AreAllSubmarinesDestroyed())
        {
            LevelComplete();
        }
    }


}
