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

    public void SummonEnemy()
    {
        Vector3 position = GetRandomPosition();
        SpawnEnemyAtPosition(position);
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

    private void SpawnEnemyAtPosition(Vector3 position)
    {
        if(CheckSpace(position)) return;
        
        if (_inactiveEnemies.Count > 0)
        {
            Enemy enemy = _inactiveEnemies.Dequeue();
            enemy.gameObject.SetActive(true);
            _activeEnemies.Add(enemy);
            enemy.transform.position = position;
        }
        else
        {
            InstantiateEnemyAtCords(position);
        }
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
        return (Physics.CheckBox(position + Vector3.up, new Vector3(0f, 0f, checkSize), Quaternion.identity));
    }

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void InstantiateEnemyAtCords(Vector3 position)
    {
        GameObject reference = Instantiate(enemyPrefab, position, Quaternion.identity);
        Enemy referEnemy = reference.GetComponent<Enemy>();
        
        _activeEnemies.Add(referEnemy);
        
        reference.GetComponent<EnemyNavigator>().SetTarget(_playerTransform);
        referEnemy.SetEnemyManager(this);
    }
}
