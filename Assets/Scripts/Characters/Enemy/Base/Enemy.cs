using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Enemy : MonoBehaviour, /*IDamageable,*/ IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    [SerializeField] public float CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set ; }
    public bool IsFacingRight { get ; set; } = true;
        
    #region State Machine Variables
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }  
    public EnemyIdleState   IdleState { get; set; }
    public bool IsAggroed { get; set ; }
    public bool IsWithinStrikingDistance { get ; set ; }

    #endregion

    #region SO Variables
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;


    public EnemyIdleSOBase EnemyIdleBaseInstance {  get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    #endregion


    #region Idle Variables
    
    #endregion

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this,StateMachine);
    }

    private void Start()
    {


        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);
        


        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();

    }
    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    //#region Health/Die Functions
    //public void Die()
    //{
    //    Destroy(gameObject,2f);
    //}

    //public void Damage(float damageAmount)
    //{
    //    CurrentHealth -= damageAmount;
    //    if (CurrentHealth < 0)
    //    {
    //        Die();
    //    }
    //}
    //#endregion

    #region Movement Functions
    public void MoveEnemy(Vector2 velocity)
    {
       
        RB.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if(IsFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else if(!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }
    #endregion

    #region Animation Triggers 

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);  
    }


    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }
    #endregion

    #region Distance Checks
    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetWithinStrikingDistance(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }
    #endregion
}
