using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (_rigidbody2D != null)
        {
            _rigidbody2D.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }
    }
}
