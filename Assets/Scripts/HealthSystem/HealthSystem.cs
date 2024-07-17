using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    public int maxHealth = 10;
    public int currentHealth = 10;

    public Animator animator;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int amount)
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
