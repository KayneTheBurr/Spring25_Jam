using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;

    InputManager inputManager;
    public PlayerManager player;

    [Header("Player Movement Values")]
    public Vector2 move_Input;
    public float moveAmount;
    public float vert_Amount;
    public float horz_Amount;

    [Header("Attack")]
    public bool attack_Input;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = GetComponent<PlayerManager>();
    }
    private void OnEnable()
    {
        if(inputManager == null)
        {
            Debug.Log(1);
            inputManager = new InputManager();
            inputManager.Enable();

            inputManager.PlayerMovement.Movement.performed += i => move_Input = i.ReadValue<Vector2>();

            inputManager.PlayerActions.Attack.performed += i => attack_Input = true;

        }
    }
    private void Update()
    {
        HandleAllInputs();
    }
    private void HandleAllInputs()
    {
        HandleMovement();
        HandleAttacking();
    }
    private void HandleMovement()
    {
        if (player == null) return;

        vert_Amount = move_Input.y;
        horz_Amount = move_Input.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(vert_Amount) + Mathf.Abs(horz_Amount));

        if (moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5 && moveAmount <= 1)
        {
            moveAmount = 1.0f;
        }
    }
    private void HandleAttacking()
    {
        if (attack_Input)
        {
            attack_Input = false;

            player.playerCombatManager.PerformBasicAttack();

        }
    }
}
