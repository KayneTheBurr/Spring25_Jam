using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnerManager : MonoBehaviour
{
    // List of all enemy spawners
    public List<EnemySpawner> spawners;

    // Number of enemies to spawn per cycle.
    public int enemiesPerCycle = 3;

    // Cycle duration in seconds.
    public float cycleTime = 10f;

    // Calculated time between spawns.
    private float spawnInterval;

    // Timer to accumulate time.
    private float spawnTimer = 0f;

    void Start()
    {
        // Calculate the interval between enemy spawns.
        spawnInterval = cycleTime / enemiesPerCycle;
        spawners = new List<EnemySpawner>(FindObjectsOfType<EnemySpawner>());
    }

    void Update()
    {
        // Increase our timer by the elapsed time.
        spawnTimer += Time.deltaTime;

        // If our timer exceeds the calculated spawnInterval, then attempt a spawn.
        if (spawnTimer >= spawnInterval)
        {
            // Update each spawner's ability to spawn by checking for nearby players.
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.CheckForPlayer();
            }

            // Create a list for spawners that are allowed to spawn.
            List<EnemySpawner> availableSpawners = new List<EnemySpawner>();

            foreach (EnemySpawner spawner in spawners)
            {
                // Use the canSpawn flag from each spawner.
                if (spawner.canSpawn)
                {
                    availableSpawners.Add(spawner);
                }
            }

            // If we have any available spawners, choose one at random.
            if (availableSpawners.Count > 0)
            {
                int randomIndex = Random.Range(0, availableSpawners.Count);
                availableSpawners[randomIndex].SpawnEnemy();
            }

            // Reset the timer.
            spawnTimer = 0f;
        }
    }
}
