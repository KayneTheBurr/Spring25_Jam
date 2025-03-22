using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyWeaponBehavior : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}
