using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector] public PlayerMovementManager playerMovementManager;
    [HideInInspector] public PlayerCombatManager playerCombatManager;
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public CharacterController characterController;

    [Header("Flags")]
    public bool canMove = true;
    public bool isPerformingAction = false;
    public bool isDead = false;

    //temp
    public float yAngle;

    public Transform attackDirection;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        characterController = GetComponent<CharacterController>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }
    private void Update()
    {
        playerMovementManager.AllMovement();
    }
    public FacingDirection GetPointerDirection()
    {
        float angle = attackDirection.rotation.eulerAngles.y;
        if (angle > 180) angle -= 360;

        yAngle = angle;

        if(angle > -180 && angle < -90)
        {
            return FacingDirection.FrontLeft;
        }
        else if (angle > -135 && angle < -45)
        {
            return FacingDirection.Left;
        }
        else if (angle > -90 && angle < -10)
        {
            return FacingDirection.BackLeft;
        }
        else if (angle > -45 && angle < 45)
        {
            return FacingDirection.Back;
        }
        else if (angle > 0 && angle < 90)
        {
            return FacingDirection.BackRight;
        }
        else if (angle > 45 && angle < 135)
        {
            return FacingDirection.Right;
        }
        else if (angle > 90 && angle < 180)
        {
            return FacingDirection.FrontRight;
        }
        else
        {
            return FacingDirection.Front;
        }
    }
}
