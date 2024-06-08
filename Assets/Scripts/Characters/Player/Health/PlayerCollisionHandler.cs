using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem va chạm có phải với đạn của enemy không
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            // Lấy ra component đạn của enemy để lấy thông tin gây sát thương
            BulletDamage enemyBullet = collision.gameObject.GetComponent<BulletDamage>();
            if (enemyBullet != null)
            {
                // Trừ máu của player dựa trên lượng sát thương từ đạn
                playerHealth.TakeDamage(enemyBullet.Damage);
                // Xóa đạn sau khi va chạm
                Destroy(collision.gameObject);
            }
        }
        //đụng phải enemy 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Trừ máu của player khi va chạm với enemy
            playerHealth.TakeDamage(2);
        }
        ////dụng phải trap
        //if (collision.gameObject.CompareTag("Trap"))
        //{
        //    // Trừ máu của player khi va chạm với bẫy
        //    playerHealth.TakeDamage(1);
        //}
    }
}
