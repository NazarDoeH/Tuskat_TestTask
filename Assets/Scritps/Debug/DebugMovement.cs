using UnityEngine;

public class DebugMovement : Movement
{
    //Used for debugging
    [SerializeField] private Vector2 input;

    protected override Vector2 GetInput()
    {
        return input;
    }
}
