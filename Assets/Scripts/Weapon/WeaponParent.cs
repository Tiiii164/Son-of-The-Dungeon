using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class WeaponParent : MonoBehaviour
{
    [SerializeField] Transform circleOrigin;
    [SerializeField] float radius;
    public SpriteRenderer _characterRenderer, _weaponRenderer;

    public Animator _animator;
    public float delay = .3f;
    private bool attackBlocked; 

    
    public Vector2 PointerPosition { get; set; }
    public bool IsAttacking { get;private set; }
    private void Update()
    {
        FlipAndRotateWeapon();
    }

    private void FlipAndRotateWeapon()
    {
        if(IsAttacking)
            return;
        
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        SortLayerOfWeapon();
    }

    private void SortLayerOfWeapon()
    {
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            _weaponRenderer.sortingOrder = _characterRenderer.sortingOrder - 1;
        }
        else
        {
            _weaponRenderer.sortingOrder = _characterRenderer.sortingOrder + 1;
        }
    }

    public void Attack()
    {
        if (attackBlocked) 
            return;

        _animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        //vẽ cái hình tròn 
        Gizmos.color = Color.blue;
        // nếu thằng circleOrigin bằng null thì thằng position = vecter3.zero nếu không thì thằng position = circleOrigin.position;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }
    //public void DetectColliders()
    //{
    //    //check coi quýnh trúng thì add logic
    //    foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
    //    {
    //        Debug.Log(collider.name);
    //        Health health;
    //        if(health = collider.GetComponent<Health>())
    //        {
    //            health.GetHit(1, transform.parent.gameObject);
    //        }
    //    }
    //}
}
