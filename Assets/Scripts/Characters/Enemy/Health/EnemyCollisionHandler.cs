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
        if (collision.gameObject.CompareTag("PlayerMelee"))
        {
            // Trừ máu của enemy khi va chạm với vũ khí cận chiến của player
            enemyHealth.TakeDamage(2);
        }
    }
}
