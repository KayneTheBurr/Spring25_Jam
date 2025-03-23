using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    Rigidbody rb;
    public float upBounceMultiplyer = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(Vector3.up * upBounceMultiplyer, ForceMode.Impulse);
    }
}
