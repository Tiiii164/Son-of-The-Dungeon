using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    [SerializeField] private AudioClip[] damageSoundClips;
    private MessageSpawner _messageSpawner;
    private DamageFlash _damageFlash;
    private ExperienceManager _experienceManager;
    //private Rigidbody2D rigidbody;
    private CapsuleCollider2D _capsuleCollider2d;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _damageFlash = GetComponent<DamageFlash>();
        _messageSpawner = GetComponent<MessageSpawner>();
        //rigidbody = GetComponent<Rigidbody2D>();
        _experienceManager = FindObjectOfType<ExperienceManager>();
        _capsuleCollider2d = GetComponent<CapsuleCollider2D>(); 
    }
    public override void TakeDamage(int amount)
    {
        //chạy animation ăn dame, đẩy lùi chớp chớp, văng máu các kiểu
        _messageSpawner.SpawnMessage(amount.ToString());
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
        _capsuleCollider2d.enabled = false;
        _experienceManager.AddExperience(5);
        Destroy(gameObject, 0.5f);

    }
}
