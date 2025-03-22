using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;

    public string lastAnimation;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
     public void PlayTargetAnimation(string animationToPlay)
    {
        lastAnimation = animationToPlay;

        player.animator.CrossFade(animationToPlay, 0.2f);
        player.isPerformingAction = true;
    }


}
