using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector] public PlayerMovementManager playerMovementManager;
    [HideInInspector] public PlayerCombatManager playerCombatManager;
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterController characterController;

    public bool canMove = true;
    public bool isPerformingAction = false;
    
    public bool isDead = false;
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
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }
    private void Update()
    {
        playerMovementManager.AllMovement();
    }

}
