using UnityEngine;

public class EnemyMovment : Movement
{
    [SerializeField] private EnemyNavigator navigator;
    
    protected override Vector2 GetInput()
    {
        return navigator.GetTargetDirection();;
    }
}
