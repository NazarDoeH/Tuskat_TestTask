using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private Material _defaultMaterial;
 
    private EnemyManager _enemyManager;
    
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void IndicateClosest(bool isClosest)
    {
        _meshRenderer.material = isClosest ? _selectedMaterial : _defaultMaterial;
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        _enemyManager = manager;
    }

    public void Kill()
    {
        ResetToDefault();
        _enemyManager.ReturnEnemy(this);
    }

    private void ResetToDefault()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        IndicateClosest(false);
    }
}
