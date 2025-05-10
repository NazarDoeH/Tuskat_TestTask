using UnityEngine;

public class EnemyNavigator : MonoBehaviour
{
    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public Vector2 GetTargetDirection()
    {
        Vector3 dir = _target.position - transform.position;
        return new Vector2(-dir.z, dir.x).normalized;
    }
}
