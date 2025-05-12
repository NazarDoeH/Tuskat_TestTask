using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    private Vector2 _playerInput;
    public void ReceiveMovement(InputAction.CallbackContext context)
    {
        _playerInput = context.action.ReadValue<Vector2>();
    }

    protected override Vector2 GetInput()
    {
        return _playerInput;
    }
}
