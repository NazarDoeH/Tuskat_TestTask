using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet parameters")]
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime = 5f;

    //Schedule self-destruction after a certain time
    void Start()
    {
        Destroy(this.gameObject, lifeTime); 
    }

    //Initialize movement
    private void Update()
    {
        transform.Translate(Vector3.back * (speed * Time.deltaTime));
    }

    //Damage the player on hit
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
