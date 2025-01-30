using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    [Header("Level Configuration")]
    public LevelData currentLevel; // Assign the level data asset in the inspector
    public EnemySpawner enemySpawner; // Assign the EnemySpawner in the inspector

    private int enemiesSpawned = 0;
    private int enemiesDefeated = 0;

    private void Awake()
    {
        if(Instance == this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (currentLevel != null)
        {
            Debug.Log($"Starting Level: {currentLevel.levelName}");
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            Debug.LogError("No LevelData assigned to LevelManager!");
        }
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
            Debug.Log("Level Complete!");
            // Trigger level completion events, load next level, etc.
        }
    }
}