using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Enemy prefab
    public GameObject enemyPrefab;

    // Bool to allow spawning.
    public bool canSpawn = true;

    // Layer which we want to check for collisions.
    public LayerMask playerLayer;

    // Radius in which we want to check for a player collision.
    public float radiusCheck;

    // Instantiates an enemy at this location
    public void SpawnEnemy()
    {
        // Instantiate enemyPrefab at the spawner's position and rotation.
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    public void CheckForPlayer()
    {
        // Check for any collissions with the player layer within the radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusCheck, playerLayer);
        canSpawn = colliders.Length == 0; // If no player colliders, then canSpawn is true.
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusCheck);
    }
}
