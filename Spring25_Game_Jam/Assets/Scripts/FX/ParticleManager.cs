using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance { get; private set; }

    public List<GameObject> particles = new List<GameObject>();
    public List<GameObject> vfx = new List<GameObject>();

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

    public void spawnParticles(GameObject prefab, Vector3 pos, Quaternion rot, Transform parent)
    {
        GameObject newParticles = Instantiate(prefab, pos, rot, parent);
    }
    public GameObject spawnParticleObject(GameObject prefab, Vector3 pos, Quaternion rot, Transform parent)
    {
        GameObject newParticles = Instantiate(prefab, pos, rot, parent);
        return newParticles;
    }
    public void spawnVFX(GameObject prefab, Vector3 pos, Quaternion rot, Transform parent, float destroyDelay)
    {
        GameObject newVFX = Instantiate(prefab, pos, rot, parent);
        Destroy(newVFX, destroyDelay);
    }
    public GameObject spawnVFXObject(GameObject prefab, Vector3 pos, Quaternion rot, Transform parent)
    {
        GameObject newVFX = Instantiate(prefab, pos, rot, parent);
        return newVFX;
    }
}
