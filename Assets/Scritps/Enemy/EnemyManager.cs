using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    [SerializeField] private float innerRadius;
    [SerializeField] private float outerRadius;

    [SerializeField] private float checkSize = 0.75f;
    
    private Transform _playerTransform;

    private List<Enemy> _activeEnemies = new List<Enemy>();
    private Queue<Enemy> _inactiveEnemies = new Queue<Enemy>();

    private int _tryCount;
    private const int MaxTryCount = 5;
    
    public void SummonEnemy()
    {
        _tryCount = 0;
        TrySpawnEnemy();
    }

    public void ReturnEnemy(Enemy enemy)
    {
        if (_activeEnemies.Remove(enemy))
        {
            enemy.gameObject.SetActive(false);
            _inactiveEnemies.Enqueue(enemy);
        }
    }

    public Enemy GetClosestEnemy()
    {
        if(_activeEnemies.Count == 0) return null;
        
        float minDistance = float.MaxValue; 
        Enemy closestEnemy = null;
        foreach (Enemy enemy in _activeEnemies)
        {
            float distance = Vector3.Distance(_playerTransform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
    
    private void TrySpawnEnemy()
    {
        while (_tryCount < MaxTryCount)
        {
            Vector3 position = GetRandomPosition();
            if (CheckSpace(position))
            {
                SpawnEnemyAtPosition(position);
                return;
            }

            _tryCount++;
        }
    }

    private void SpawnEnemyAtPosition(Vector3 position)
    {
        Enemy enemy;

        if (_inactiveEnemies.Count > 0)
        {
            enemy = _inactiveEnemies.Dequeue();
            enemy.gameObject.SetActive(true);
        }
        else
        {
            enemy = InstantiateEnemyAtCords(position);
        }

        _activeEnemies.Add(enemy);
        enemy.transform.position = position;
    }

    private Vector3 GetRandomPosition()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float radius = Random.Range(outerRadius, outerRadius);
        
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);
        
        return new Vector3(
            _playerTransform.position.x + x,
            1f,
            _playerTransform.position.z + y);
    }

    private bool CheckSpace(Vector3 position)
    {
        bool hasGround = Physics.Raycast(position, Vector3.down, out RaycastHit hit, 5f) && hit.collider.CompareTag("Ground");
        bool isClear = !Physics.CheckBox(position + Vector3.up, Vector3.one * checkSize);
        return hasGround && isClear;
    }

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private Enemy InstantiateEnemyAtCords(Vector3 position)
    {
        GameObject reference = Instantiate(enemyPrefab, position, Quaternion.identity);
        Enemy referEnemy = reference.GetComponent<Enemy>();
        
        reference.GetComponent<EnemyNavigator>().SetTarget(_playerTransform);
        referEnemy.SetEnemyManager(this);

        return referEnemy;
    }
}
