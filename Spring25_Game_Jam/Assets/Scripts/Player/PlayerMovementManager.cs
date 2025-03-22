using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementManager : MonoBehaviour
{
    PlayerManager player; 
    private float vertMovement, horzMovement, moveAmount;

    public Vector3 lastDirection;

    public Vector3 moveDirection;
    public float gravityMagnitude = 20;
    private Vector3 gravityForce;
    public float moveSpeed;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void GetMovementInputs()
    {
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

        if(PlayerInputManager.instance.moveAmount > 0.5f)
        {
            player.characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        else if(PlayerInputManager.instance.moveAmount <= 0.5f)
        {
            player.characterController.Move(moveDirection * moveSpeed / 2 * Time.deltaTime);
        }

        player.characterController.Move(gravityForce * Time.deltaTime);
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
