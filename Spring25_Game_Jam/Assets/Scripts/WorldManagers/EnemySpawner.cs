using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemies = new List<GameObject>();

    // Bool to allow spawning.
    public bool canSpawn = true;

    // Layer which we want to check for collisions.
    public LayerMask playerLayer;

    // Radius in which we want to check for a player collision.
    public float radiusCheck;

    // Instantiates an enemy at this location
    public void SpawnEnemy()
    {
        int randomNumber = Random.Range(0, enemies.Count);
        // Instantiate enemyPrefab at the spawner's position and rotation.
        Instantiate(enemies[randomNumber], transform.position, transform.rotation);
    }
    public void CheckForPlayer()
    {
        // Check for any collissions with the player layer within the radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusCheck, playerLayer);
        canSpawn = colliders.Length == 0; // If no player colliders, then canSpawn is true.
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusCheck);
    }
}
