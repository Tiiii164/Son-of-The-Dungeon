using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Straight-Single Projectile", menuName = "Enemy Logic/Attack Logic/Straight Single Projectile")]
public class AttackSingleStraightProjectile : EnemyAttackSOBase
{
    public Rigidbody2D BulletPrefab;
    private float _timer;
    private float _exitTimer;


    [SerializeField] float _timeBetweenShots = 2f;
    [SerializeField] float _bulletSpeed = 10f;
    [SerializeField] float _timeTillExit = 3f;
    [SerializeField] float _distanceToCountExit = 3f;


    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        //enemy.MoveEnemy(Vector2.zero);
        Vector2 directionToPlayer = (playerTransform.position - enemy.transform.position).normalized;

        //// Rotate the enemy to face the player
        //float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        //enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        // Flip the enemy to face the player
        if (directionToPlayer.x > 0 && enemy.transform.localScale.x < 0)
        {
            enemy.transform.localScale = new Vector3(Mathf.Abs(enemy.transform.localScale.x), enemy.transform.localScale.y, enemy.transform.localScale.z);
        }
        else if (directionToPlayer.x < 0 && enemy.transform.localScale.x > 0)
        {
            enemy.transform.localScale = new Vector3(-Mathf.Abs(enemy.transform.localScale.x), enemy.transform.localScale.y, enemy.transform.localScale.z);
        }
        // Shooting logic
        if (_timer > _timeBetweenShots)
        {
            _timer = 0f;
            Vector2 dir = (playerTransform.position - enemy.transform.position).normalized;
            Rigidbody2D bullet = GameObject.Instantiate(BulletPrefab, enemy.transform.position, Quaternion.identity);
            bullet.velocity = dir * _bulletSpeed;
        }
        // Exit logic
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

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
