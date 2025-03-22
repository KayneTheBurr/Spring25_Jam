using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public PlayerMovementManager playerMovementManager;
    [HideInInspector] public PlayerCombatManager playerCombatManager;
    [HideInInspector] public CharacterController characterController;

    public bool canMove = true;
    public bool isPerformingAction = false;
    public Transform attackDirection;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
    }
    private void Update()
    {
        playerMovementManager.AllMovement();
    }

}
