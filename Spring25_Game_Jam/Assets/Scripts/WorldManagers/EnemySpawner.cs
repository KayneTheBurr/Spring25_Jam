using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Enemy prefab
    public GameObject enemyPrefab;

    // Instantiates an enemy at this location
    public void SpawnEnemy()
    {
        // Instantiate enemyPrefab at the spawner's position and rotation.
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
