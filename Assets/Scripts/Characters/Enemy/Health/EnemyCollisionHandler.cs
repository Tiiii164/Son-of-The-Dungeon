using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem va chạm có phải với đạn của player không
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // Lấy ra component đạn của player để lấy thông tin gây sát thương
            BulletDamage playerBullet = collision.gameObject.GetComponent<BulletDamage>();
            if (playerBullet != null)
            {
                // Trừ máu của enemy dựa trên lượng sát thương từ đạn
                enemyHealth.TakeDamage(playerBullet.Damage);
                // Xóa đạn sau khi va chạm
                Destroy(collision.gameObject);

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // đụng trúng vũ khí cận chiến
        //if (collision.gameObject.CompareTag("PlayerMelee"))
        //{
        //    if (collision.gameObject.name == "AggroRadius" && collision.gameObject.name == "StrikingDistance")
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        // Trừ máu của enemy khi va chạm với vũ khí cận chiến của player
        //        enemyHealth.TakeDamage(2);
        //    }
        //}

        if (collision.gameObject.CompareTag("PlayerMelee"))
        {
            // Kiểm tra nếu collider là CapsuleCollider2D
            if (collision is CapsuleCollider2D)
            {
                // Trừ máu của enemy khi va chạm với CapsuleCollider2D của player
                enemyHealth.TakeDamage(2);
            }
            else
            {
                // Bỏ qua nếu collider không phải là CapsuleCollider2D
                return;
            }
        }

        IKnockBackable knockbackable = GetComponent<IKnockBackable>();
        if (knockbackable != null)
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            float force = 2f;
            knockbackable.ApplyKnockBack(direction, force);
        }
    }
}
