using UnityEngine;

[RequireComponent (typeof(Collider))]
public class InnerTentBehavior : MonoBehaviour
{
    public GameObject tentFace;
    private MeshRenderer tentMesh;
    private WorldStateMaterialSwap materialSwap;

    private void Awake()
    {
        tentMesh = tentFace.GetComponent<MeshRenderer>();
        materialSwap = tentFace.GetComponent<WorldStateMaterialSwap>();
    }

    private void OnTriggerEnter(Collider col)
    {
        tentMesh.enabled = false;
    }

    private void OnTriggerExit(Collider col)
    {
        tentMesh.enabled = true;
        materialSwap.OnGameStateChanged();
    }
}
