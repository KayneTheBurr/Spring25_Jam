using System.Collections;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager player;

    [Header("Flags")]
    public bool canCombo = false;
    public bool isChargingAttack = false;

    [Header("Attack Values")]
    public float attackRadius = 5f;
    public float attackRange = 5f;
    public float vfxOffest = 2f;
    public float slamExtraOffset = 2f;
    public float slamDelayTime = 2;
        
    [Header("Attack Damage Values")]
    public float lightAttack01_Damage;
    public float lightAttack02_Damage;
    public float lightAttack03_Damage;
    public float heavyAttack_01_Damage;

    [Header("Front Light Attacks")]
    [SerializeField] string light_Attack_Front_01 = "Front_Light_Attack_01";
    [SerializeField] string light_Attack_Front_02 = "Front_Light_Attack_02";
    [SerializeField] string light_Attack_Front_03 = "Front_Light_Attack_03";

    [Header("Front Heavy Attacks")]
    [SerializeField] string heavy_Attack_Front_01 = "Front_Heavy_Attack_01";

    [Header("Back Light Attacks")]
    [SerializeField] string light_Attack_Back_01 = "Back_Light_Attack_01";
    [SerializeField] string light_Attack_Back_02 = "Back_Light_Attack_02";
    [SerializeField] string light_Attack_Back_03 = "Back_Light_Attack_03";

    [Header("Back Heavy Attacks")]
    [SerializeField] string heavy_Attack_Back_01 = "Back_Heavy_Attack_01";

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void DealDamage(string attackAnim)
    {
        FacingDirection direction = player.GetPointerDirection();
        float damageDone = 0;
        

        //determnine the damage of the hit based on the attackAnim string passed in 
        if(attackAnim == light_Attack_Back_01 || attackAnim == light_Attack_Front_01)
        {
            damageDone = lightAttack01_Damage;
        }
        else if(attackAnim == light_Attack_Back_02 || attackAnim == light_Attack_Front_02)
        {
            damageDone = lightAttack02_Damage;
        }
        else if(attackAnim == light_Attack_Back_03 || attackAnim == light_Attack_Front_03)
        {
            damageDone = lightAttack03_Damage;
        }
        else if(attackAnim == heavy_Attack_Back_01 || attackAnim == heavy_Attack_Front_01)
        {
            damageDone = heavyAttack_01_Damage;
        }
        else
        {
            damageDone = lightAttack01_Damage;
        }

        //use the facing direction to detect for enemies in that direction
        Transform pointerDirection = player.attackDirection;
        Vector3 directionToAttack = pointerDirection.forward;

        Debug.DrawLine(pointerDirection.position, pointerDirection.position + directionToAttack * attackRange, Color.red, 2f);
        RaycastHit[] enemiesHit = Physics.SphereCastAll(pointerDirection.position, attackRadius, directionToAttack, attackRange);

        foreach(var hit in enemiesHit)
        {
            HealthBehavior healthLogic = hit.transform.GetComponent<HealthBehavior>();
            if(healthLogic != null)
            {
                healthLogic.TakeDMG(damageDone);
            }
        }
        //spawn a vfx prefab that direction using the Particle Manager 
        if (attackAnim == light_Attack_Back_01 || attackAnim == light_Attack_Front_01
            || attackAnim == light_Attack_Back_02 || attackAnim == light_Attack_Front_02)
        {
            ParticleManager.instance.spawnParticles(ParticleManager.instance.particles[4],
            pointerDirection.position + directionToAttack * vfxOffest, Quaternion.LookRotation(directionToAttack), player.transform);
        }

        if (attackAnim == light_Attack_Back_03 || attackAnim == light_Attack_Front_03)
        {
            StartCoroutine(PlaySlamEffect(pointerDirection, directionToAttack));
        }

        //Play a sfx depending on what attack was used

    }
    public IEnumerator PlaySlamEffect(Transform pointerDirection, Vector3 directionToAttack)
    {
        yield return new WaitForSeconds(slamDelayTime);

        ParticleManager.instance.spawnParticles(ParticleManager.instance.particles[6],
            pointerDirection.position + directionToAttack * (vfxOffest + slamExtraOffset), Quaternion.LookRotation(directionToAttack), player.transform);
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

                if (player.playerAnimationManager.lastAnimation == light_Attack_Back_01 || player.playerAnimationManager.lastAnimation == light_Attack_Front_01)
                {
                    Debug.Log("Play second attack front here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_02, true);
                    DealDamage(light_Attack_Front_02);
                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Back_02 || player.playerAnimationManager.lastAnimation == light_Attack_Front_02)
                {
                    Debug.Log("Play third attack front here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_03, true);
                    DealDamage(light_Attack_Front_03);
                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Back_03 || player.playerAnimationManager.lastAnimation == light_Attack_Front_03)
                {
                    Debug.Log("Play first attack front here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_01, true);
                    DealDamage(light_Attack_Front_01);
                }
            }
            else if (!player.isPerformingAction)
            {
                Debug.Log("Start attack chain front ");
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_01, true);
                DealDamage(light_Attack_Front_01);

            }
            player.isPerformingAction = true;
        }
        else if(!facingFront)
        {
            //if we are attacking and can combo, the next attack in combo chain
            if (canCombo && player.isPerformingAction)
            {
                canCombo = false;

                if (player.playerAnimationManager.lastAnimation == light_Attack_Back_01 || player.playerAnimationManager.lastAnimation == light_Attack_Front_01)
                {
                    Debug.Log("Play second attack back here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Back_02, true);
                    DealDamage(light_Attack_Back_02);

                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Back_02 || player.playerAnimationManager.lastAnimation == light_Attack_Front_02)
                {
                    Debug.Log("Play third attack back here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Back_03, true);
                    DealDamage(light_Attack_Back_03);

                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Back_03 || player.playerAnimationManager.lastAnimation == light_Attack_Front_03)
                {
                    Debug.Log("Play first attack back here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Back_01, true);
                    DealDamage(light_Attack_Back_01);

                }
            }
            else if (!player.isPerformingAction)
            {
                Debug.Log("Start attack chain back");
                player.playerAnimationManager.PlayTargetAnimation(light_Attack_Back_01, true);
                DealDamage(light_Attack_Back_01);

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
        bool facingFront = true;
        
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
        if (facingFront)
        {
            if (!player.isPerformingAction)
            {
                Debug.Log("Play heavy attack front");
                player.playerAnimationManager.PlayTargetAnimation(heavy_Attack_Front_01, true);
                DealDamage(heavy_Attack_Front_01);
            }
        }
        else if (!facingFront)
        {
            if (!player.isPerformingAction)
            {
                Debug.Log("Play heavy attack back");
                player.playerAnimationManager.PlayTargetAnimation(heavy_Attack_Back_01, true);
                DealDamage(heavy_Attack_Back_01);
            }
        }
    }
    public void EnableCanCombo()
    {
        //Debug.Log("Enable can combo");
        canCombo = true;
    }
    public void DisableCanCombo()
    {
        canCombo &= false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,attackRadius);
    }
}
