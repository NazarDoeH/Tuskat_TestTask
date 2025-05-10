using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDellay = 1.5f;
    
    [SerializeField] private GameObject bulletPrefab;
    
    private bool _canAttack = true;

    void Update()
    {
        if (_canAttack)
        {
            StartCoroutine(AttackCorutine());
            Attack();
        }
    }

    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    IEnumerator AttackCorutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackDellay);
        _canAttack = true;
    }

}
