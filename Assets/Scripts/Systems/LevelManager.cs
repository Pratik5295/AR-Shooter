using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    [Header("Level Configuration")]
    public List<LevelData> levels; // List of level data assets
    public EnemySpawner enemySpawner; // Assign the EnemySpawner in the inspector

    private int currentLevelIndex = 0;
    private LevelData currentLevel;

    private int enemiesSpawned = 0;
    private int enemiesDefeated = 0;
    private bool levelInProgress = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GameManager.Instance.GetCurrentState() != GameState.GAME) return;

        StartLevel(0); // Start from the first level
    }

    public void StartLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count)
        {
            SetStatusText("Invalid level index!");
            return;
        }

        currentLevelIndex = levelIndex;
        currentLevel = levels[currentLevelIndex];
        enemiesSpawned = 0;
        enemiesDefeated = 0;
        levelInProgress = true;

        //Update the spawner
        enemySpawner.SetRooster(currentLevel.enemyPrefabs);

        SetStatusText($"Starting Level: {currentLevel.levelName}");
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < currentLevel.enemyCount)
        {
            enemySpawner.SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(currentLevel.spawnInterval);
        }
    }

    // Call this when an enemy dies
    public void EnemyDefeated()
    {
        enemiesDefeated++;

        if (enemiesDefeated >= currentLevel.enemyCount)
        {
            SetStatusText("Level Complete!");
            levelInProgress = false;
            PlayNextLevel();
        }
    }

    public void PlayNextLevel()
    {
        if (currentLevelIndex + 1 < levels.Count)
        {
            StartLevel(currentLevelIndex + 1);
        }
        else
        {
            SetStatusText("All levels completed! Game Over or Restart.");
        }
    }

    public void RestartLevel()
    {
        Debug.Log($"Restarting Level: {currentLevel.levelName}");
        StartLevel(currentLevelIndex);
    }

    private void SetStatusText(string message)
    {
        MainMenuManager.Instance.SetStatusText(message);
    }
}
