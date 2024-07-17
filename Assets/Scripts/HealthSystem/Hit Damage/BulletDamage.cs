using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour, IDamage,IKnockBackable
{
    public int Damage { get; set; }
    public int damage;
    public float KnockBackForce { get; set; }
    public float knockBackForce = 5f;

    private void Start()
    {
        Damage = damage;
        KnockBackForce = knockBackForce;
    }
    public void ApplyKnockBack(Vector2 direction, float force)
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            KnockBack playerKnockBack = collision.gameObject.GetComponent<KnockBack>();
            if (playerHealth != null)
            {
                // Trừ máu của player dựa trên lượng sát thương từ đạn
                playerHealth.TakeDamage(Damage);
                Destroy(gameObject); // Hủy viên đạn ngay sau khi gây sát thương
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

                playerKnockBack.ApplyKnockback(knockbackDirection, KnockBackForce);
                //Debug.Log(KnockBackForce);

            }
        }
    }
}
