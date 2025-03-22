using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public float timeToDie = 4f;
    
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        transform.LookAt(FindFirstObjectByType<PlayerMovementManager>().transform);

        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        Destroy(gameObject, timeToDie);
    }

    public void HitSomething()
    {
        Destroy(gameObject);
    }
}
