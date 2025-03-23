using UnityEngine;

[RequireComponent (typeof(Collider))]
public class CameraCullingMask : MonoBehaviour
{
    public Material normalSpriteMaterial;
    public Material transparentSpriteMaterial;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Enter");

        if(col.gameObject.layer == 6 || col.gameObject.layer == 7)
        {
            // 6 is player
            // 7 is enemy
            // do nothing

            return;
        }

        Debug.Log("Enter non player/enemy");

        SpriteRenderer colliderSP = FindRendererFromCollider(col);

        if(colliderSP != null)
        {
            Debug.Log("Enter sprite");
            colliderSP.material = transparentSpriteMaterial;
            return;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 6 || col.gameObject.layer == 7)
        {
            // 6 is player
            // 7 is enemy
            // do nothing

            return;
        }

        SpriteRenderer colliderSP = FindRendererFromCollider(col);

        if (colliderSP != null)
        {
            colliderSP.material = normalSpriteMaterial;
        }
    }

    private SpriteRenderer FindRendererFromCollider(Collider col)
    {
        SpriteRenderer sp = col.gameObject.GetComponent<SpriteRenderer>();
        if (sp != null) { return sp; }

        sp = col.transform.GetComponentInChildren<SpriteRenderer>();
        return sp;
    }
}
