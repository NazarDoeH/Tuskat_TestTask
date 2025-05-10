using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime = 5f;

    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.back * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
