using System.Collections;
using UnityEngine;
using Cinemachine;

public class PlayerHealth : HealthSystem
{
    private DamageFlash _damageFlash;
    private CinemachineImpulseSource _impulseSource;

    [SerializeField] private int damage = 1;
    [SerializeField] private int healthPerLevel = 2;
    [SerializeField] private int damagePerLevel = 1;
    private HealthBarManager _healthBarManager;
    private Transform respawnTransform;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _damageFlash = GetComponent<DamageFlash>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _healthBarManager = FindObjectOfType<HealthBarManager>();
    }

    public override void TakeDamage(int amount)
    {
        CameraShakeManager.instance.CameraShake(_impulseSource);
        _damageFlash.CallDamageFlash();
        base.TakeDamage(amount);
        _healthBarManager.UpdateHealthBar();
    }

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("Died");
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2); // Thời gian chờ trước khi chuyển scene
        SceneController.instance.LoadScene("Lobby");

        yield return new WaitForSeconds(0.1f); // Thời gian chờ để đảm bảo scene được tải hoàn tất

        Transform respawnPoint = SceneController.instance.respawnPoint;
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
        }

        ExperienceManager experienceManager = FindObjectOfType<ExperienceManager>();
        if (experienceManager != null)
        {
            experienceManager.ResetExperience();
        }

        //maxHealth = initialMaxHealth;
        //damage = initialDamageValue;
        currentHealth = maxHealth; // Hồi đầy máu
        animator.ResetTrigger("Died");
        animator.Rebind();
        _healthBarManager.UpdateHealthBar();
    }

    public void LevelUp()
    {
        maxHealth += healthPerLevel;
        damage += damagePerLevel;
        currentHealth = maxHealth;
        Debug.Log("Level Up! New Max Health: " + maxHealth + ", New Damage: " + damage);
    }

    public int GetDamage()
    {
        return damage;
    }
}
