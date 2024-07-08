using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    [SerializeField] private AudioClip[] damageSoundClips;
    
    private DamageFlash _damageFlash;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _damageFlash = GetComponent<DamageFlash>();

        //rigidbody = GetComponentInChildren<Rigidbody2D>();
    }
    public override void TakeDamage(float amount)
    {
        //chạy animation ăn dame, đẩy lùi chớp chớp các kiểu

        _damageFlash.CallDamageFlash();
        base.TakeDamage(amount);
        //âm thanh đao đớn 
        SoundFXManager.Instance.PlayRandomSoundFXClip(damageSoundClips, transform ,1f); 
    }

    protected override void Die()
    {
        base.Die();
        //animation chết ngắt
        animator.SetTrigger("Died");
        Destroy(gameObject,0.5f);
        //rigidbody.velocity = Vector3.zero;
    }
}
