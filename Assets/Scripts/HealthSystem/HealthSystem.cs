using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    public float maxHealth = 10;
    [SerializeField] float currentHealth = 10;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name +" has " + currentHealth.ToString() + " HP");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " has died!");
    }

    
}
