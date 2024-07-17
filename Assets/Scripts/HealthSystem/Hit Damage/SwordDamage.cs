using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour,IKnockBackable
{

    private PlayerHealth playerHealth;
    public float KnockBackForce { get; set ;}
    public float knockBackForce;
    private bool canKnockBack = true;

    private void Awake()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }
    private void Start()
    {
        KnockBackForce = knockBackForce;

    }
    public void ApplyKnockBack(Vector2 direction, float force)
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") )
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            KnockBack enemyKnockBack = collision.gameObject.GetComponent<KnockBack>();
            enemyHealth.TakeDamage(playerHealth.GetDamage());
            if ( canKnockBack)
            {
                // Trừ máu của enemy dựa trên lượng sát thương từ kiếm
               
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                enemyKnockBack.ApplyKnockback(knockbackDirection, KnockBackForce);
                Debug.Log("Knockback applied: " + knockbackDirection * KnockBackForce);
                StartCoroutine(ResetKnockBack());
            }
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
    private IEnumerator ResetKnockBack()
    {
        canKnockBack = false;
        yield return new WaitForSeconds(3f); // Đợi 3 giây
        canKnockBack = true;
    }
}
