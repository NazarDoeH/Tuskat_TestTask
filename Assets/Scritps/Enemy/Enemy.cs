using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Enemy : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject deathEffect;
 
    private EnemyManager _enemyManager;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void SetEnemyManager(EnemyManager manager)
    {
        _enemyManager = manager;
    }

    //Handles the enemy's death
    public void Kill()
    {
        ResetToDefault();
        _enemyManager.ReturnEnemy(this);
    }

    //Resets the enemy's state and spawns a death effect
    private void ResetToDefault()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        _rigidbody.linearVelocity = Vector3.zero;
    }
}
