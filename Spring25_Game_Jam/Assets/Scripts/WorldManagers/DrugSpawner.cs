using UnityEngine;

public class DrugSpawner : MonoBehaviour
{
    // Drug prefab assigned in the Inspector.
    public GameObject drugPrefab;

    // Bool to allow spawning.
    public bool canSpawn = true;

    // Layer for player detection.
    public LayerMask playerLayer;

    // Radius to check for a nearby player.
    public float radiusCheck;

    // Spawns the drug.
    public void SpawnDrug()
    {
        Instantiate(drugPrefab, transform.position, transform.rotation);
    }

    // Checks if a player is nearby.
    public void CheckForPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusCheck, playerLayer);
        canSpawn = colliders.Length == 0;
    }
}
