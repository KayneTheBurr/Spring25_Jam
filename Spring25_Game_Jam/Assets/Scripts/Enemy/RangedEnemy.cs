using UnityEngine;

public class RangedEnemy : Enemy
{
    //Variables Here
    public Transform rangedWeaponTransform;
    public GameObject bulletPrefab;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }

    public override void AttackUpdate()
    {
        base.AttackUpdate();
    }

    public override void SetAttackState()
    {
        // spawn bullet
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = rangedWeaponTransform.position;

        DrugState worldState = WorldGameState.instance.GetWorldState();
        if(worldState == DrugState.Bouba) { SFXManager.instance.PlayBubbleShootSound(); }
        else
        {
            // kiki shoot sound
        }

        base.SetAttackState();
    }
}
