using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-And-Summon", menuName = "Enemy Logic/Attack Logic/Attack And Summon")]
public class AttackAndSummon : EnemyAttackSOBase
{
    private float _timer;
    private float _exitTimer;
    private float _summonTimer;
    private Animator _animator;
    private bool isLowHealth = false;
    public GameObject bulletPrefab; // Prefab của đạn
    public int bulletCount = 10;    // Số lượng đạn
    public float radius = 1.0f;

    [SerializeField] private float _timeBetweenTaunts = 2f;
    [SerializeField] private float _tauntSpeed = 20000f;
    [SerializeField] private float _timeTillExit = 3f;
    [SerializeField] private float _distanceToCountExit = 3f;
    [SerializeField] private float _summonInterval = 10f; // Thời gian giữa các lần triệu hồi
    [SerializeField] private GameObject summonPrefab; // Prefab sẽ được triệu hồi
    [SerializeField] private int summonCount = 2; // Số lượng triệu hồi mỗi lần

    private EnemyHealth _enemyHealth;

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
        _animator = gameObject.GetComponentInChildren<Animator>();
        _enemyHealth = gameObject.GetComponent<EnemyHealth>();
    }

    public override void DoEnterLogic()
    {
        _timer = 0f;
        _exitTimer = 0f;
        _summonTimer = 0f;
        _animator.SetBool("IsAttacking", false); // Đặt lại giá trị bool khi vào trạng thái
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        _animator.SetBool("IsAttacking", false); // Đặt lại giá trị bool khi ra khỏi trạng thái
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 directionToPlayer = (playerTransform.position - enemy.transform.position);

        // Xoay hướng boss để đối mặt với người chơi
        if (directionToPlayer.x > 0 && enemy.transform.localScale.x < 0)
        {
            enemy.transform.localScale = new Vector3(Mathf.Abs(enemy.transform.localScale.x), enemy.transform.localScale.y, enemy.transform.localScale.z);
        }
        else if (directionToPlayer.x < 0 && enemy.transform.localScale.x > 0)
        {
            enemy.transform.localScale = new Vector3(-Mathf.Abs(enemy.transform.localScale.x), enemy.transform.localScale.y, enemy.transform.localScale.z);
        }

        if (_enemyHealth.currentHealth < _enemyHealth.maxHealth * 0.5f && !isLowHealth)
        {
            isLowHealth = true;
        }

        if (isLowHealth)
        {
            // Triệu hồi prefabs mỗi 3 giây
            _summonTimer += Time.deltaTime;
            if (_summonTimer >= _summonInterval)
            {
                SpawnBullets();
                SummonPrefabs();
                enemy.MoveEnemy(directionToPlayer * 10f);
                _summonTimer = 0f; // Reset lại summon timer
            }
        }
        else
        {
            // Tấn công người chơi nếu chưa ở trạng thái máu thấp
            if (_timer > _timeBetweenTaunts)
            {
                Vector2 moveVector = directionToPlayer * _tauntSpeed;
                Debug.Log($"Move Vector: {moveVector}");
                enemy.MoveEnemy(moveVector);

                _animator.SetBool("IsAttacking", true);
                _timer = 0f; // Reset lại timer
            }
            else
            {
                _animator.SetBool("IsAttacking", false);
            }
        }

        // Logic thoát khỏi trạng thái này
        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            _exitTimer = 0;
        }

        _timer += Time.deltaTime;
    }

    void SpawnBullets()
    {
        float angleStep = 360f / bulletCount; // Góc giữa mỗi viên đạn
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // Tính toán vị trí của viên đạn
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletVector = new Vector3(bulletDirX, bulletDirY, 0);
            Vector3 bulletMoveDirection = (bulletVector - transform.position).normalized * radius;

            // Tạo viên đạn
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletMoveDirection.x, bulletMoveDirection.y);

            // Cập nhật góc
            angle += angleStep;
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
        _timer = 0f;
        _exitTimer = 0f;
        _summonTimer = 0f;
        isLowHealth = false;
    }

    private void SummonPrefabs()
    {
        for (int i = 0; i < summonCount; i++)
        {
            // Triệu hồi prefab tại vị trí ngẫu nhiên gần boss
            Vector3 spawnPosition = enemy.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Instantiate(summonPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
