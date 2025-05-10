using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;

    public void AttackClosest(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed) enemyManager.GetClosestEnemy()?.Kill();
    }
}
