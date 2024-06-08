using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float Damage = 10f;

    private void Update()
    {
        Destroy(gameObject,3f);
    }
}
