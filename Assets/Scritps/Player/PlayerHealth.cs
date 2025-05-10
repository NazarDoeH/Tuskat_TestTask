using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    
    public void TakeDamage(float damage)
    {
        DealDamage(damage);
        Debug.Log("Damaged");
    }

    private void DealDamage(float damage)
    {
        if (health - damage > 0f)
        {
            health -= damage;
        }
        else
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        
    }
}
