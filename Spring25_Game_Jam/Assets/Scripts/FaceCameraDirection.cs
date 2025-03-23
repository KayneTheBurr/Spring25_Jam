using UnityEngine;

public class FaceCameraDirection : MonoBehaviour
{
    
    void Update()
    {
        transform.forward = -Camera.main.transform.forward;
    }
}
