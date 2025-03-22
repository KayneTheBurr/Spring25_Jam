using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager player;

    public float elapsedTime = 0f;
    public float comboTimer = 0.5f;
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
        elapsedTime += Time.deltaTime;



    }
    public void PerformBasicAttack()
    {
        //if we are attacking and can combo, the next attack in combo chain
        if(canCombo && player.isPerformingAction)
        {
            canCombo = false;

            if (player.playerAnimationManager.lastAnimation == light_Attack_01)
            {
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_02);
            }
            else if (player.playerAnimationManager.lastAnimation == light_Attack_02)
            {
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_03);
            }
            else if(player.playerAnimationManager.lastAnimation == light_Attack_03)
            {
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_01);
            }
        }
        else if(!player.isPerformingAction)
        {
            player.playerAnimationManager.PlayTargetAnimation(light_Attack_01);
        }
    }
    public void PerformHeavyAttack()
    {

    }
    public void EnableCanCombo()
    {
        canCombo = true;
    }
    public void DisableCanCombo()
    {
        canCombo &= false;
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
    }
}
