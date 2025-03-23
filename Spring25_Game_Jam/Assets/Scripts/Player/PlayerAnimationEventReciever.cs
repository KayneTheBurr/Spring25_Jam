using UnityEngine;

public class PlayerAnimationEventReciever : MonoBehaviour
{
    PlayerManager player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerManager>();
    }
    public void EnableCanCombo()
    {
        player.playerCombatManager.EnableCanCombo();
    }
    public void SpinAttackDamage()
    {
        player.playerCombatManager.DealSpinDamage();
    }
    public void DestroyChargingFX()
    {
        player.playerCombatManager.DestroyChargingEffects();
    }
    public void StartSpinVFX()
    {
        player.playerCombatManager.StartSpinEffect();
    }
    public void PlayTinkSound()
    {
        SFXManager.instance.PlayBigHitTink();
    }
    
}
