using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public Enemy enemyPrefab; 
    public int maxEnemies = 5;     // Maximum number of enemies to spawn at a time
    public float spawnInterval = 3f; // Time interval between spawns
    public Vector3 spawnAreaSize = new Vector3(3f, 0f, 3f); // Area within which enemies spawn

    private int currentEnemyCount = 0; // Track the number of spawned enemies
    private float spawnTimer = 0f;     // Timer for spawn intervals

    [SerializeField]
    private List<Enemy> enemyRooster;

    void OnDrawGizmosSelected()
    {
        // Draw the spawn area in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

    private void Start()
    {
        enemyRooster = new List<Enemy>();
    }

    void Update()
    {
        if (GameManager.Instance.GetCurrentState() != GameState.GAME) return;

        //if (currentEnemyCount < maxEnemies)
        //{
        //    spawnTimer += Time.deltaTime;

        //    if (spawnTimer >= spawnInterval)
        //    {
        //        SpawnEnemy();
        //        spawnTimer = 0f;
        //    }
        //}
    }

    public void SpawnEnemy()
    {
        // Generate a random position within the spawn area
        Vector3 randomPosition = GetRandomPositionInArea();

        //Get Random enemy to spawn
        enemyPrefab = GetRandomEnemy();

        // Spawn the enemy
        Enemy enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

        // Ensure the spawned enemy counts towards the current limit
        currentEnemyCount++;

        // Subscribe to the enemy's destruction event
        if (enemy != null)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.destroyOnDeath = true;
                enemyHealth.ResetHealth(); // Ensure the enemy spawns with full health
            }
        }

        if (enemy != null)
        {
            enemy.SetTarget(GameManager.Instance.PlayerObject.transform);
        }
    }

    private Vector3 GetRandomPositionInArea()
    {
        // Generate a random position within the bounds of the spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            GameManager.Instance.PlayerObject.transform.position.y,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        return transform.position + randomPosition;
    }

    public void SetRooster(List<Enemy> enemies)
    {
        enemyRooster = enemies;
    }

    private Enemy GetRandomEnemy()
    {
        if (enemyRooster == null || enemyRooster.Count == 0)
        {
            Debug.LogWarning("Enemy roster is empty!");
            return null;
        }

        int randomIndex = Random.Range(0, enemyRooster.Count);
        return enemyRooster[randomIndex];
    }
}
