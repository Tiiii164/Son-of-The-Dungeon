using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        //chạy animation ăn dame, đẩy lùi chớp chớp các kiểu
    }

    protected override void Die()
    {
        base.Die();
        //animation chết ngắt
    }
}
