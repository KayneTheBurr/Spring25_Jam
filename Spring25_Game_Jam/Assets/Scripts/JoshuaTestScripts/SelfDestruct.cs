using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    public float upwardVelocity = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Destroys the GameObject after 2 seconds.
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
