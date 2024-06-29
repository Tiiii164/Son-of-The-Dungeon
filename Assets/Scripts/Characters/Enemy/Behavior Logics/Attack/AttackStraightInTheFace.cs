using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Attack-Straight-In The Face", menuName = "Enemy Logic/Attack Logic/Straight In The Face")]
public class AttackStraightInTheFace : EnemyAttackSOBase
{
    
    private float _timer;
    private float _exitTimer;
    private Animator _animator;

    [SerializeField] float _timeBetweenTaunts = 2f;
    [SerializeField] float _tauntSpeed = 2f;
    [SerializeField] float _timeTillExit = 3f;
    [SerializeField] float _distanceToCountExit = 3f;

   
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {

        _timer = 0f;
        _exitTimer = 0f;
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

        // Taunt logic
        if (_timer > _timeBetweenTaunts)
        {
            // Charge towards the player
            enemy.MoveEnemy(directionToPlayer * _tauntSpeed);
            _animator.SetTrigger("IsAttacking");
            _timer = 0f; // Reset the timer after the taunt
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
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    public override void ResetValues()
    {
        base.ResetValues();
        _timer = 0f;
        _exitTimer = 0f;
    }
}