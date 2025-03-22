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

    [Header("Light Attack")]
    public bool lightAttack_Input;

    [Header("Heavy Attacks")]
    public bool charged_HA_Input;

    [Header("Qued Inputs")]
    [SerializeField] float que_Input_Timer = 0;
    [SerializeField] float default_Que_Input_Timer = 0.5f;
    [SerializeField] bool input_Que_Active = false;
    [SerializeField] bool qued_LA_Input = false;
    [SerializeField] bool qued_HA_input = false;

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

            //movement
            inputManager.PlayerMovement.Movement.performed += i => move_Input = i.ReadValue<Vector2>();

            //Light Attack input
            inputManager.PlayerActions.LightAttack.performed += i => lightAttack_Input = true;

            //Heavy Attack Inputs
            inputManager.PlayerActions.HeavyAttack.performed += i => lightAttack_Input = true;
            inputManager.PlayerActions.Charged_HA.performed += i => lightAttack_Input = true;
            inputManager.PlayerActions.HeavyAttack.performed += i => lightAttack_Input = true;

            //qued inputs
            inputManager.PlayerActions.Qued_LA.performed += i => lightAttack_Input = true;
            inputManager.PlayerActions.Qued_HA.performed += i => lightAttack_Input = true;



        }
    }
    private void Update()
    {
        HandleAllInputs();
    }
    private void HandleAllInputs()
    {
        HandleMovement();
        HandleBasicAttack();
        HandleChargedAttack();
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
    private void HandleBasicAttack()
    {
        if (lightAttack_Input)
        {
            lightAttack_Input = false;

            player.playerCombatManager.PerformBasicAttack();

        }
    }
    private void HandleChargedAttack()
    {
        if (charged_HA_Input)
        {
            charged_HA_Input = false;

            player.playerCombatManager.PerformBasicAttack();

        }
    }
}
