using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] float _movementSpeed = 3f;


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

        // Kiểm tra nếu không còn aggro thì chuyển trạng thái
        if (!enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
            return;
        }

        Vector2 moveDirection = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(moveDirection * _movementSpeed);

        if (enemy.IsWithinStrikingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
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
