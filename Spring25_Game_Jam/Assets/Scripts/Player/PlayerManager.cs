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
    [HideInInspector] public HealthBehavior healthBehavior;

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
        healthBehavior = GetComponentInChildren<HealthBehavior>();
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
    public void HealMeFool(float healAmount)
    {
        healthBehavior.Heal(healAmount);
    }
    public FacingDirection GetPointerDirection()
    {
        float angle = attackDirection.rotation.eulerAngles.y;
        if (angle > 180) angle -= 360;

        yAngle = angle;

        if(angle > -170 && angle < -100)
        {
            return FacingDirection.FrontLeft;
        }
        else if (angle > -125 && angle < -55)
        {
            return FacingDirection.Left;
        }
        else if (angle > -80 && angle < -10)
        {
            return FacingDirection.BackLeft;
        }
        else if (angle > -35 && angle < 35)
        {
            return FacingDirection.Back;
        }
        else if (angle > 10 && angle < 80)
        {
            return FacingDirection.BackRight;
        }
        else if (angle > 55 && angle < 125)
        {
            return FacingDirection.Right;
        }
        else if (angle > 100 && angle < 170)
        {
            return FacingDirection.FrontRight;
        }
        else
        {
            return FacingDirection.Front;
        }
    }
}
