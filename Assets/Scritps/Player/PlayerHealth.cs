using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float health = 100f;
    [Header("Effects")]
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private GameObject deathEffect;
    [Header("References")]
    [SerializeField] private SceneManager sceneManager; //Must be reworked to avoid using a direct reference to SceneManager
    
    public void TakeDamage(float damage)
    {
        DealDamage(damage);
    }

    private void DealDamage(float damage)
    {
        if (health - damage > 0f)
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
            health -= damage;
        }
        else
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        sceneManager.RestartSceneWithDelay();
        Instantiate(damageEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
