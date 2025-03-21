using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance { get; private set; }

    public List<GameObject> particles = new List<GameObject>();

    private void Awake()
    {
        //set singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void spawnParticles(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        GameObject newParticles = Instantiate(prefab, pos, rot);
    }

    public void spawnVFX(GameObject prefab, Vector3 pos, Quaternion rot, float destroyDelay)
    {
        GameObject newVFX = Instantiate(prefab, pos, rot);
        Destroy(newVFX, destroyDelay);
    }
}
