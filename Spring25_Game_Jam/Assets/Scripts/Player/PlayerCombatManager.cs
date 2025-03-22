using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager player;

    public bool canCombo = false;

    public bool isChargingAttack = false;

    [SerializeField] string light_Attack_Front_01 = "Front_Light_Attack_01";
    [SerializeField] string light_Attack_Front_02 = "Front_Light_Attack_02";
    [SerializeField] string light_Attack_Front_03 = "Front_Light_Attack_03";

    [SerializeField] string heavy_Attack_Front_01 = "Front_Heavy_Attack_01";

    [SerializeField] string light_Attack_Back_01 = "Back_Light_Attack_01";
    [SerializeField] string light_Attack_Back_02 = "Back_Light_Attack_02";
    [SerializeField] string light_Attack_Back_03 = "Back_Light_Attack_03";

    [SerializeField] string heavy_Attack_Back_01 = "Back_Heavy_Attack_01";

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
        //cant attack in kiki mode 
        //if (WorldGameState.GetWorldState() == DrugState.Kikki) return;
        bool facingFront = true;
        string animationToPlay = "";

        switch (player.playerAnimationManager.myDirection)
        {
            case FacingDirection.Front:
            case FacingDirection.Left:
            case FacingDirection.Right:
            case FacingDirection.FrontLeft:
            case FacingDirection.FrontRight:
                facingFront = true;
                break;

            case FacingDirection.Back:
            case FacingDirection.BackLeft:
            case FacingDirection.BackRight:
                facingFront = false;
                break;
        }
        //this is all for FRONT facing, need to do the same for facing back as well 
        if (facingFront)
        {
            //if we are attacking and can combo, the next attack in combo chain
            if (canCombo && player.isPerformingAction)
            {
                canCombo = false;

                if (player.playerAnimationManager.lastAnimation == light_Attack_Front_01)
                {
                    Debug.Log("Play second attack here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_02, true);
                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Front_02)
                {
                    Debug.Log("Play third attack here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_03, true);
                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Front_03)
                {
                    Debug.Log("Play first attack here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_01, true);
                }
            }
            else if (!player.isPerformingAction)
            {
                Debug.Log("Play first attack here");
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_01, true);
            }
            player.isPerformingAction = true;
        }
        else if(!facingFront)
        {
            //if we are attacking and can combo, the next attack in combo chain
            if (canCombo && player.isPerformingAction)
            {
                canCombo = false;

                if (player.playerAnimationManager.lastAnimation == light_Attack_Front_01)
                {
                    Debug.Log("Play second attack here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_02, true);
                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Front_02)
                {
                    Debug.Log("Play third attack here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_03, true);
                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Front_03)
                {
                    Debug.Log("Play first attack here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_01, true);
                }
            }
            else if (!player.isPerformingAction)
            {
                Debug.Log("Play first attack here");
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_01, true);
            }
            player.isPerformingAction = true;
        }
    }
    private void HandleChargingAttack()
    {
        if(isChargingAttack)
        {
            Debug.Log("charging attack");
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
            player.playerAnimationManager.PlayTargetAnimation(heavy_Attack_Front_01, true);
        }
    }
    public void EnableCanCombo()
    {
        Debug.Log("Enable can combo");
        canCombo = true;
    }
    public void DisableCanCombo()
    {
        canCombo &= false;
    }
}
