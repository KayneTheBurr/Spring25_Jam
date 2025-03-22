using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;

    public string lastAnimation;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void Update()
    {
        HandleSpriteFlipping();
        HandleMovementAnimation();
    }
    public void PlayTargetAnimation(string animationToPlay)
    {
        lastAnimation = animationToPlay;

        player.animator.CrossFade(animationToPlay, 0.2f);
        player.isPerformingAction = true;
    }
    private void HandleSpriteFlipping()
    {
        float direction = player.GetPointerDirection();
    }
    private void HandleMovementAnimation()
    {
        float direction = player.GetPointerDirection();

        //if the player is moving do this 
        if (player.playerMovementManager.moveAmount > 0)
        {
            
        }
        //the player isnt moving, so figure out what idle to play
        else
        {

        }
    }

}
