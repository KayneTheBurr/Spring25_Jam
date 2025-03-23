using UnityEngine;
using System.Collections.Generic;

public class DrugSpawnManager : MonoBehaviour
{
    // List of all drug spawners in the scene.
    public List<DrugSpawner> spawners;

    // Cycle counter: on first cycle, cycleNumber is 0.
    public int cycleNumber = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnDrugs();
        }
    }
    void Start()
    {
        // Find all DrugSpawner components in the scene.
        spawners = new List<DrugSpawner>(FindObjectsByType<DrugSpawner>());

        // Subscribe to the world state change event.
        WorldGameState.worldStateChanged += OnWorldStateChanged;
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks.
        WorldGameState.worldStateChanged -= OnWorldStateChanged;
    }

    // This function is called whenever the world state changes.
    void OnWorldStateChanged()
    {
        // Check if the new world state is Kikki.
        if (WorldGameState.GetWorldState() == DrugState.Kikki)
        {
            SpawnDrugs();
        }
    }

    // Spawns a cycle of drugs.
    public void SpawnDrugs()
    {
        // Calculate how many drugs to spawn this cycle:
        // It spawns 7 on the first cycle, then 6, then 5, ... but not less than 1.
        int drugsToSpawn = Mathf.Max(1, 7 - cycleNumber);

        // Loop to spawn the calculated number of drugs.
        for (int i = 0; i < drugsToSpawn; i++)
        {
            // Update each spawner's canSpawn flag.
            foreach (DrugSpawner spawner in spawners)
            {
                spawner.CheckForPlayer();
            }

            // Create a list for spawners that are allowed to spawn.
            List<DrugSpawner> availableSpawners = new List<DrugSpawner>();
            foreach (DrugSpawner spawner in spawners)
            {
                if (spawner.canSpawn)
                {
                    availableSpawners.Add(spawner);
                }
            }

            // If any spawners are available, choose one at random to spawn a drug.
            if (availableSpawners.Count > 0)
            {
                int randomIndex = Random.Range(0, availableSpawners.Count);
                availableSpawners[randomIndex].SpawnDrug();
            }
            else
            {
                Debug.Log("Uhhhh...where are the spawners dude?");
            }
        }

        // Increment the cycle counter for the next world state change.
        cycleNumber++;
    }
}
