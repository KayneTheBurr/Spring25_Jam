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
}
