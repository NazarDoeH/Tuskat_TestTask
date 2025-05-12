using UnityEngine;

public class EnemyMovement : Movement
{
    [SerializeField] private EnemyNavigator navigator;
    
    protected override Vector2 GetInput()
    {
        return navigator.GetTargetDirection();
    }
}
