using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthBehavior : MonoBehaviour
{
    public float maxHealthPoints = 100f;
    public float currentHealthPoints = 100f; 
    private bool isPlayer = false;

    private bool isAlive = true;

    void Awake()
    {
        if (GetComponent<PlayerMovementManager>() != null)
        {
            isPlayer = true;
        }

        currentHealthPoints = maxHealthPoints;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!isAlive) { return; }

        if (isPlayer)
        {
            // look for enemy colliders
            if (col.gameObject.GetComponent<Enemy>() != null)
            {
                TakeDMG(col.gameObject.GetComponent<Enemy>().dmgFromHitbox);
                return;
            }
        }
        else
        {
            // look for player attack colliders

        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!isAlive) { return; }

        if (isPlayer)
        {
            // look for enemy attack colliders
            if (col.gameObject.GetComponent<EnemyWeaponBehavior>() != null)
            {
                TakeDMG(col.gameObject.GetComponent<EnemyWeaponBehavior>().damage);
                
                if(col.gameObject.GetComponent<Bullet>() != null)
                {
                    col.gameObject.GetComponent<Bullet>().HitSomething();
                }

                return;
            }
        }
        else
        {

        }
    }

    public void Heal(float healAmount)
    {
        if (!isAlive) { return; }

        currentHealthPoints += healAmount;
        // show heal animations

        if (currentHealthPoints > maxHealthPoints)
        {
            currentHealthPoints = maxHealthPoints;
        }
    }

    public void TakeDMG(float dmgAmount)
    {
        if (!isAlive) { return; }

        currentHealthPoints -= dmgAmount;
        // show hurt animations

        if(currentHealthPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        GetComponent<Collider>().enabled = false;

        // set death animation
    }
}
