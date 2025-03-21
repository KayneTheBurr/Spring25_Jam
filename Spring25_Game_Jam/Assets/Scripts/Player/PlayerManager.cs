using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public PlayerMovementManager playerMovementManager;
    [HideInInspector] public PlayerCombatManager playerCombatManager;
    [HideInInspector] public CharacterController characterController;
    public bool canMove = true;
    public Transform attackDirection;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
    }
    private void Update()
    {
        playerMovementManager.AllMovement();
    }

}
