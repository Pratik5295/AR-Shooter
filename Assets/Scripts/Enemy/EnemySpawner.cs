using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public Enemy enemyPrefab; 
    public int maxEnemies = 5;     // Maximum number of enemies to spawn at a time
    public float spawnInterval = 3f; // Time interval between spawns
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f); // Area within which enemies spawn

    private int currentEnemyCount = 0; // Track the number of spawned enemies
    private float spawnTimer = 0f;     // Timer for spawn intervals

    void OnDrawGizmosSelected()
    {
        // Draw the spawn area in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

    void Update()
    {
        if (currentEnemyCount < maxEnemies)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        // Generate a random position within the spawn area
        Vector3 randomPosition = GetRandomPositionInArea();

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
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        return transform.position + randomPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optional: Start spawning enemies when the player enters the trigger area
            enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optional: Stop spawning enemies when the player leaves the trigger area
            enabled = false;
        }
    }
}
