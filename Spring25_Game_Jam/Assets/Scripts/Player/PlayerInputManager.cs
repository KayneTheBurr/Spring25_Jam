using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;

    InputManager inputManager;
    public PlayerManager player;

    public bool isCharging = false;

    [Header("Player Movement Values")]
    public Vector2 move_Input;
    public float moveAmount;
    public float vert_Amount;
    public float horz_Amount;

    [Header("Light Attack")]
    public bool lightAttack_Input;

    [Header("Heavy Attacks")]
    public bool charged_HA_Input;
    public bool heavyAttack_Input;

    [Header("Qued Inputs")]
    [SerializeField] float que_Input_Timer = 0;
    [SerializeField] float default_Que_Input_Timer = 0.5f;
    [SerializeField] bool input_Que_Active = false;
    [SerializeField] bool qued_LA_Input = false;
    [SerializeField] bool qued_HA_Input = false;

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
            inputManager = new InputManager();
            inputManager.Enable();

            //movement
            inputManager.PlayerMovement.Movement.performed += i => move_Input = i.ReadValue<Vector2>();

            //Light Attack input
            inputManager.PlayerActions.LightAttack.performed += i => lightAttack_Input = true;

            //Heavy Attack Inputs
            inputManager.PlayerActions.HeavyAttack.performed += i => heavyAttack_Input = true;
            inputManager.PlayerActions.Charged_HA.performed += i => charged_HA_Input = true;
            inputManager.PlayerActions.Charged_HA.canceled += i => charged_HA_Input = false;

            //qued inputs
            inputManager.PlayerActions.Qued_LA.performed += i => QuedInput(ref qued_LA_Input);
            inputManager.PlayerActions.Qued_HA.performed += i => QuedInput(ref qued_HA_Input);

        }
    }
    private void Update()
    {
        HandleAllInputs();
    }
    private void HandleAllInputs()
    {
        HandleMovement();
        HandleLightAttack();
        HandleHeavyAttack();
        HandleChargedHeavyAttack();
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
    private void HandleLightAttack()
    {
        if (lightAttack_Input)
        {
            lightAttack_Input = false;

            player.playerCombatManager.PerformBasicAttack();
        }
    }
    private void HandleHeavyAttack()
    {
        if (heavyAttack_Input)
        {
            heavyAttack_Input = false;

            player.playerCombatManager.PerformHeavyAttack();
        }
    }
    private void HandleChargedHeavyAttack()
    {
        
        if (player.isPerformingAction)
        {
            if (charged_HA_Input)
            {
                isCharging = true;
            }
            else
            {
                isCharging = false;
            }

            player.playerCombatManager.isChargingAttack = isCharging;

            //Debug.Log("CHECK CHARGING: " + isCharging); 
        }
    }
    private void QuedInput(ref bool quedInput)
    {
        //only allows queing one attack at a time 
        qued_HA_Input = false;
        qued_LA_Input = false;

        if(player.isPerformingAction)
        {
            quedInput = true;
            que_Input_Timer = default_Que_Input_Timer;
            input_Que_Active = true;
        }
    }
    private void HandleQuedInputs()
    {
        if(input_Que_Active)
        {
            if(que_Input_Timer > 0)
            {
                que_Input_Timer -= Time.deltaTime;
                ProcessQuedInputs();
            }
            else
            {
                qued_HA_Input = false;
                qued_LA_Input = false;

                input_Que_Active = false;
                que_Input_Timer = 0;
            }
        }
    }
    private void ProcessQuedInputs()
    {
        if (player.isDead) return;

        if (qued_LA_Input) lightAttack_Input = true;
        if (qued_HA_Input) heavyAttack_Input = true;
    }
}
