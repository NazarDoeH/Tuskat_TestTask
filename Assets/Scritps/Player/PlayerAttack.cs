using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject failedAttackEffect;
    
    [Header("References")]
    [SerializeField] private EnemyManager enemyManager;

    // Attempts to attack the closest enemy when the input is performed. Spawns a failure effect if no enemy is found
    public void AttackClosest(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Enemy closestEnemy = enemyManager.GetClosestEnemy();
            
            if (closestEnemy == null)
            {
                Instantiate(failedAttackEffect, transform.position, Quaternion.identity);
                return;
            }
            
            closestEnemy.Kill();
        }
    }
}
