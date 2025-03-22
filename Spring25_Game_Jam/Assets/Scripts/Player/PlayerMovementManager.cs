using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementManager : MonoBehaviour
{
    PlayerManager player; 
    public float vertMovement, horzMovement, moveAmount;

    public Vector3 lastDirection;

    public Vector3 moveDirection;
    public float gravityMagnitude = 20;
    private Vector3 gravityForce;
    public float moveSpeed;
    public bool isMoving = false;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void GetMovementInputs()
    {
        //get values from input manager
        vertMovement = PlayerInputManager.instance.vert_Amount;
        horzMovement = PlayerInputManager.instance.horz_Amount;
        moveAmount = PlayerInputManager.instance.moveAmount;
    }
    public void AllMovement()
    {
        if (!player.canMove) return;

        gravityForce.y = -gravityMagnitude;

        GetMovementInputs();

        //determine which direction to move here
        moveDirection = player.transform.forward * vertMovement;
        moveDirection += player.transform.right * horzMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if(PlayerInputManager.instance.moveAmount > 0.5f) //move full speed
        {
            player.characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        else if(PlayerInputManager.instance.moveAmount <= 0.5f) //move half speed
        {
            player.characterController.Move(moveDirection * moveSpeed / 2 * Time.deltaTime);
        }

        player.characterController.Move(gravityForce * Time.deltaTime); //manual gravity force 

        HandleRotation(moveDirection);
    }
    private void HandleRotation(Vector3 moveDirection)
    {
        if(moveDirection.magnitude == 0)
        {
            player.attackDirection.rotation = Quaternion.LookRotation(lastDirection);

        }
        else
        {
            lastDirection = moveDirection;
            player.attackDirection.rotation = Quaternion.LookRotation(lastDirection);
        }

    }
}
