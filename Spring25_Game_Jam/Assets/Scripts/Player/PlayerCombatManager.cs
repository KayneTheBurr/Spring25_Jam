using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager player;

    public bool canCombo = false;

    public bool isChargingAttack = false;

    [SerializeField] string light_Attack_01 = "Light_Attack_01";
    [SerializeField] string light_Attack_02 = "Light_Attack_03";
    [SerializeField] string light_Attack_03 = "Light_Attack_02";

    [SerializeField] string heavy_Attack_01 = "Heavy_Attack_01";

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void Update()
    {
        HandleChargingAttack();
    }
    public void PerformBasicAttack()
    {
        //if we are attacking and can combo, the next attack in combo chain
        if(canCombo && player.isPerformingAction)
        {
            canCombo = false;

            if (player.playerAnimationManager.lastAnimation == light_Attack_01)
            {
                Debug.Log("Play second attack here");
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_02);
            }
            else if (player.playerAnimationManager.lastAnimation == light_Attack_02)
            {
                Debug.Log("Play third attack here");
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_03);
            }
            else if(player.playerAnimationManager.lastAnimation == light_Attack_03)
            {
                Debug.Log("Play first attack here");
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_01);
            }
        }
        else if(!player.isPerformingAction)
        {
            Debug.Log("Play first attack here");
            player.playerAnimationManager.PlayTargetAnimation(light_Attack_01);
        }

        player.isPerformingAction = true;

    }
    private void HandleChargingAttack()
    {
        if(isChargingAttack)
        {
            player.animator.SetBool("IsCharging", true);
        }
        else
        {
            player.animator.SetBool("IsCharging", false);
        }
    }
    public void PerformHeavyAttack()
    {
        if (!player.isPerformingAction)
        {
            Debug.Log("Play heavy attack");
            player.playerAnimationManager.PlayTargetAnimation(heavy_Attack_01);

            player.isPerformingAction = true;
        }
    }
    public void EnableCanCombo()
    {
        canCombo = true;
    }
    public void DisableCanCombo()
    {
        canCombo &= false;
    }
}
