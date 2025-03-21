using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementManager : MonoBehaviour
{
    PlayerManager player; 
    private float vertMovement, horzMovement, moveAmount;

    private Vector3 moveDirection;

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
        if(player.canMove)
        {
            GetMovementInputs();
        }
        if (!player.canMove) return;

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


    }
}
