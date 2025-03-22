using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;

    public FacingDirection myDirection = FacingDirection.Front;
    public FacingDirection lastDirection = FacingDirection.Front;
    public string lastAnimation;

    [Header("Move Direction Animations")]
    [SerializeField] string front_Bouba_Move = "Bouba_Front_Move";
    [SerializeField] string front_Kiki_Move = "Kiki_Front_Move";
    [SerializeField] string back_Bouba_Move = "Bouba_Back_Move";
    [SerializeField] string back_Kiki_Move = "Kiki_Back_Move";

    [Header("Idle Direction Animaitions")]
    [SerializeField] string front_Bouba_Idle = "Bouba_Front_Idle";
    [SerializeField] string front_Kiki_Idle = "Kiki_Front_Idle";
    [SerializeField] string back_Bouba_Idle = "Bouba_Back_Idle";
    [SerializeField] string back_Kiki_Idle = "Kiki_Back_Idle";

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void Update()
    {
        HandleFacingDirection();
        HandleDrugState();
        HandleSpriteFlipping();
        HandleMovementAnimation();
    }
    private void HandleDrugState()
    {
        if (WorldGameState.GetWorldState() == DrugState.Bouba)
        {
            player.animator.SetBool("IsCharging", true);
        }
        else
        {
            player.animator.SetBool("IsCharging", false);
        }
    }
    private void HandleFacingDirection()
    {
        lastDirection = myDirection;
        myDirection = player.GetPointerDirection();
    }
    public void PlayTargetAnimation(string animationToPlay)
    {
        lastAnimation = animationToPlay;

        player.animator.CrossFade(animationToPlay, 0.2f);
        player.isPerformingAction = true;
    }
    private void HandleSpriteFlipping()
    {
        //if facing left, dont flip
        if(myDirection == FacingDirection.Left || myDirection == FacingDirection.BackLeft || 
            myDirection == FacingDirection.FrontLeft)
        {
            player.spriteRenderer.flipX = false;
        }
        //if facing right, do flip
        if (myDirection == FacingDirection.Right || myDirection == FacingDirection.BackRight ||
            myDirection == FacingDirection.FrontRight)
        {
            player.spriteRenderer.flipX = true;
        }
        //if directly up or down, look at previous state to determine which direction to face
        if (myDirection == FacingDirection.Back || myDirection == FacingDirection.Front)
        {
            if(lastDirection == FacingDirection.Left || lastDirection == FacingDirection.BackLeft ||
            lastDirection == FacingDirection.FrontLeft)
            {
                player.spriteRenderer.flipX = false;
            }
            else if(lastDirection == FacingDirection.Right || lastDirection == FacingDirection.BackRight ||
            lastDirection == FacingDirection.FrontRight)
            {
                player.spriteRenderer.flipX = true;
            }
        }
    }
    private void HandleMovementAnimation()
    {
        //this is just Bouba, need to copy/paste this for kiki as well 
        if (WorldGameState.GetWorldState() == DrugState.Bouba)
        {
            //if the player is moving do this 
            if (player.playerMovementManager.moveAmount > 0)
            {
                switch (myDirection)
                {
                    case FacingDirection.Front:
                    case FacingDirection.Left:
                    case FacingDirection.Right:
                    case FacingDirection.FrontLeft:
                    case FacingDirection.FrontRight:
                        PlayTargetAnimation(front_Bouba_Move);
                        break;

                    case FacingDirection.Back:
                    case FacingDirection.BackLeft:
                    case FacingDirection.BackRight:
                        PlayTargetAnimation(back_Bouba_Move);
                        break;
                }
            }
            //the player isnt moving, so figure out what idle to play
            else
            {
                switch (myDirection)
                {
                    case FacingDirection.Front:
                    case FacingDirection.Left:
                    case FacingDirection.Right:
                    case FacingDirection.FrontLeft:
                    case FacingDirection.FrontRight:
                        PlayTargetAnimation(front_Bouba_Idle);
                        break;

                    case FacingDirection.Back:
                    case FacingDirection.BackLeft:
                    case FacingDirection.BackRight:
                        PlayTargetAnimation(back_Bouba_Idle);
                        break;
                }
            }
        }
        if (WorldGameState.GetWorldState() == DrugState.Kikki)
        {
            //if the player is moving do this 
            if (player.playerMovementManager.moveAmount > 0)
            {
                switch (myDirection)
                {
                    case FacingDirection.Front:
                    case FacingDirection.Left:
                    case FacingDirection.Right:
                    case FacingDirection.FrontLeft:
                    case FacingDirection.FrontRight:
                        PlayTargetAnimation(front_Kiki_Move);
                        break;

                    case FacingDirection.Back:
                    case FacingDirection.BackLeft:
                    case FacingDirection.BackRight:
                        PlayTargetAnimation(back_Kiki_Move);
                        break;
                }
            }
            //the player isnt moving, so figure out what idle to play
            else
            {
                switch (myDirection)
                {
                    case FacingDirection.Front:
                    case FacingDirection.Left:
                    case FacingDirection.Right:
                    case FacingDirection.FrontLeft:
                    case FacingDirection.FrontRight:
                        PlayTargetAnimation(front_Kiki_Idle);
                        break;

                    case FacingDirection.Back:
                    case FacingDirection.BackLeft:
                    case FacingDirection.BackRight:
                        PlayTargetAnimation(back_Kiki_Idle);
                        break;
                }
            }
        }
    }

}
