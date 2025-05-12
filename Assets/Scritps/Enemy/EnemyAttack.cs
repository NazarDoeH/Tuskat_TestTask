using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDelay = 1.5f;
    
    [SerializeField] private GameObject bulletPrefab;
    
    private bool _canAttack;

    private void OnEnable()
    {
        StartCoroutine(AttackCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        if (!_canAttack) return;
        StartCoroutine(AttackCoroutine());
        Attack();
    }

    //Spawns a bullet at the enemy's current position and rotation
    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    IEnumerator AttackCoroutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
    }

}
