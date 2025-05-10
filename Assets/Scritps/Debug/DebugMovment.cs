using UnityEngine;

public class DebugMovment : Movement
{
    [SerializeField] private Vector2 input;

    protected override Vector2 GetInput()
    {
        return input;
    }
}
