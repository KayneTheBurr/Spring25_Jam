using UnityEngine;

public class MeleeEnemy : Enemy
{
    //Variables Here
    

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }

    public override void SetChargeUpState()
    {
        spriteAnim.SetTrigger("charge");
        base.SetChargeUpState();
    }

    public override void SetAttackState()
    {
        spriteAnim.SetTrigger("attack");

        DrugState worldState = WorldGameState.GetWorldState();
        if (worldState == DrugState.Bouba) { SFXManager.instance.PlayBearHitSound(); }
        else
        {
            // kiki shoot sound
        }

        base.SetAttackState();
    }
}
