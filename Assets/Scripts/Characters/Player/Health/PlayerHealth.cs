using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerHealth : HealthSystem
{
    private DamageFlash _damageFlash;
    private CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _damageFlash = GetComponent<DamageFlash>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();


    }
    
    public override void TakeDamage(float amount)
    {

        //chạy animation ăn dame, đẩy lùi chớp chớp, run màn hình các kiểu
        CameraShakeManager.instance.CameraShake(_impulseSource);
        _damageFlash.CallDamageFlash();
        base.TakeDamage(amount);
    }

    protected override void Die()
    {
        base.Die();
        //animation chết ngắt
        animator.SetTrigger("Died");
        Destroy(gameObject, 1.1f);
        //rigidbody.velocity = Vector3.zero;
    }
    
        
    


}
