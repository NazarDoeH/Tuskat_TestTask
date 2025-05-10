using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;

    public void AttackClosest()
    {
        enemyManager.GetClosestEnemy()?.Kill();
    }
}
