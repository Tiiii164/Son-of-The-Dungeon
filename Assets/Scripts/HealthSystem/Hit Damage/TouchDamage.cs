using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchDamage : MonoBehaviour, IDamage, IKnockBackable
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player") /*&& collision.gameObject.layer != LayerMask.NameToLayer("Layer 3")*/)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            KnockBack playerKnockBack = collision.gameObject.GetComponent<KnockBack>();
            //Debug.Log(collision.gameObject.name);
            //if(collision.gameObject.tag != ("PlayerMelee"))
            if (playerHealth != null)
            {
                // Trừ máu của player dựa trên lượng sát thương từ enemy
                playerHealth.TakeDamage(Damage);
                
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

                playerKnockBack.ApplyKnockback(knockbackDirection, KnockBackForce);
                //Debug.Log(KnockBackForce);
            }
        }
    }
}