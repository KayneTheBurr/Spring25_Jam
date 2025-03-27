using System.Collections;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerManager player;

    [Header("Flags")]
    public bool canCombo = false;
    public bool isChargingAttack = false;

    [Header("Screen Shake Values")]
    public float hitShakeAmp = 2.0f;
    public float hitShakeFreq = 0.1f;
    public float slamShakeAmp = 3f;
    public float slamShakeFreq = 0.15f;

    [Header("Attack Values")]
    public float attackRadius = 1f;
    public float attackRange = 5f;
    public float vfxOffest = 2f;
    public float slamExtraOffset = 2f;
    public float finalSlamRadius = 2f;
    public float slamDelayTime = 2;
    public float spinDamageRadius = 3f;
    public LayerMask enemyLayer;
        
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

    public GameObject currentChargingEffect;
    public GameObject currentSpinEffect;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    public void TakeDamage()
    {
        Debug.Log("take damage");

        ScreenShake.instance.shakeCam(hitShakeAmp, hitShakeFreq);
        
        if (WorldGameState.instance.GetWorldState() == DrugState.Kikki)
        {
            ParticleManager.instance.spawnParticles(ParticleManager.instance.particles[2],
                transform.position, Quaternion.identity, player.transform);
        }
        else if (WorldGameState.instance.GetWorldState() == DrugState.Bouba)
        {
            Debug.Log("Confetti Damage");
            ParticleManager.instance.spawnParticles(ParticleManager.instance.particles[1],
                transform.position, Quaternion.identity, player.transform);
        }
    }
    private void DealDamage(string attackAnim)
    {
        FacingDirection direction = player.GetPointerDirection();
        float damageDone = 0;
        attackRange = 5f;
        

        if (attackAnim == heavy_Attack_Back_01 || attackAnim == heavy_Attack_Front_01)
        {
            HandleHeavyAttackDamage();
            return;
        }

        //determnine the damage of the hit based on the attackAnim string passed in 
        if (attackAnim == light_Attack_Back_01 || attackAnim == light_Attack_Front_01)
        {
            damageDone = lightAttack01_Damage;
        }
        else if (attackAnim == light_Attack_Back_02 || attackAnim == light_Attack_Front_02)
        {
            damageDone = lightAttack02_Damage;
        }
        else if (attackAnim == light_Attack_Back_03 || attackAnim == light_Attack_Front_03)
        {
            damageDone = 0; // lightAttack03_Damage;
            attackRange = 7f;
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

        bool hitAtLeastOne = false;
        foreach(var hit in enemiesHit)
        {
            if(hit.transform.GetComponent<Enemy>() != null)
            {
                hitAtLeastOne = true; break;
            }
        }

        if (hitAtLeastOne) SFXManager.instance.PlayHitSound();

        foreach(var hit in enemiesHit)
        {
            HealthBehavior healthLogic = hit.transform.GetComponent<HealthBehavior>();
            if(healthLogic != null)
            {
                if (hit.transform.GetComponent<PlayerManager>() != null) break;

                healthLogic.TakeDMG(damageDone);
            }
        }

        //spawn a vfx prefab that direction using the Particle Manager 
        if (attackAnim == light_Attack_Back_01 || attackAnim == light_Attack_Front_01
            || attackAnim == light_Attack_Back_02 || attackAnim == light_Attack_Front_02)
        {
            //Debug.Log("Spawn vfx");

            ParticleManager.instance.spawnParticles(ParticleManager.instance.particles[4],
            pointerDirection.position + directionToAttack * vfxOffest, Quaternion.LookRotation(directionToAttack), player.transform);
        }

        if (attackAnim == light_Attack_Back_03 || attackAnim == light_Attack_Front_03)
        {
            StartCoroutine(PlaySlamEffect(pointerDirection, directionToAttack));
        }
    }
    public IEnumerator PlaySlamEffect(Transform pointerDirection, Vector3 directionToAttack)
    {
        yield return new WaitForSeconds(slamDelayTime);

        ScreenShake.instance.shakeCam(slamShakeAmp, slamShakeFreq);

        ParticleManager.instance.spawnParticles(ParticleManager.instance.particles[6],
            pointerDirection.position + directionToAttack * (vfxOffest + slamExtraOffset), Quaternion.LookRotation(directionToAttack), player.transform);


        Collider[] hitEnemies = Physics.OverlapSphere(pointerDirection.position + directionToAttack * (vfxOffest + slamExtraOffset), finalSlamRadius, enemyLayer);

        foreach (var enemy in hitEnemies)
        {

            if (enemy.GetComponent<Enemy>() != null)
            {

                enemy.GetComponent<HealthBehavior>().TakeDMG(lightAttack03_Damage);
            }
        }
    }
    private void HandleHeavyAttackDamage()
    {
        currentChargingEffect = ParticleManager.instance.spawnParticleObject(ParticleManager.instance.particles[7],
            transform.position, Quaternion.identity, player.transform);
        SFXManager.instance.PlayBigHitWindUp();
    }
    public void StartSpinEffect()
    {
        if (currentSpinEffect == null)
        {
            currentSpinEffect = ParticleManager.instance.spawnParticleObject(ParticleManager.instance.vfx[0],
                transform.position, Quaternion.identity, player.transform);
        }
        if(!SFXManager.instance.audioSource.isPlaying)
        {
            SFXManager.instance.PlayBigHitSpin();
        }
    }
    public void DealSpinDamage()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(player.transform.position, spinDamageRadius, enemyLayer);
        
        foreach (var enemy in hitEnemies)
        {
            
            if (enemy.GetComponent<Enemy>() != null)
            {
                
                enemy.GetComponent<HealthBehavior>().TakeDMG(heavyAttack_01_Damage);
            }
        }
    }
    public void DestroyChargingEffects()
    {
        if(currentChargingEffect != null)
        {
            Destroy(currentChargingEffect);
        }
    }
    private void Update()
    {

        HandleChargingAttack();
    }
    public void PerformBasicAttack()
    {
        //cant attack in kiki mode 
        if (WorldGameState.instance.GetWorldState() == DrugState.Kikki) return;
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
                    //Debug.Log("Play second attack front here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_02, true);
                    DealDamage(light_Attack_Front_02);
                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Back_02 || player.playerAnimationManager.lastAnimation == light_Attack_Front_02)
                {
                    //Debug.Log("Play third attack front here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_03, true);
                    DealDamage(light_Attack_Front_03);
                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Back_03 || player.playerAnimationManager.lastAnimation == light_Attack_Front_03)
                {
                    //Debug.Log("Play first attack front here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Front_01, true);
                    DealDamage(light_Attack_Front_01);
                }
            }
            else if (!player.isPerformingAction)
            {
                //Debug.Log("Start attack chain front ");
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
                    //Debug.Log("Play second attack back here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Back_02, true);
                    DealDamage(light_Attack_Back_02);

                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Back_02 || player.playerAnimationManager.lastAnimation == light_Attack_Front_02)
                {
                    //Debug.Log("Play third attack back here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Back_03, true);
                    DealDamage(light_Attack_Back_03);

                }
                else if (player.playerAnimationManager.lastAnimation == light_Attack_Back_03 || player.playerAnimationManager.lastAnimation == light_Attack_Front_03)
                {
                    //Debug.Log("Play first attack back here");
                    player.playerAnimationManager.PlayTargetAnimation(light_Attack_Back_01, true);
                    DealDamage(light_Attack_Back_01);

                }
            }
            else if (!player.isPerformingAction)
            {
                //Debug.Log("Start attack chain back");
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
            
            player.animator.SetBool("IsCharging", true);
        }
        else
        {
            player.animator.SetBool("IsCharging", false);
        }
    }
    public void PerformHeavyAttack()
    {
        //cant attack in kiki mode 
        if (WorldGameState.instance.GetWorldState() == DrugState.Kikki) return;

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
                //Debug.Log("Play heavy attack front");
                player.playerAnimationManager.PlayTargetAnimation(heavy_Attack_Front_01, true);
                DealDamage(heavy_Attack_Front_01);
            }
        }
        else if (!facingFront)
        {
            if (!player.isPerformingAction)
            {
                //Debug.Log("Play heavy attack back");
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
        Gizmos.DrawWireSphere(transform.position, spinDamageRadius);
    }
}
