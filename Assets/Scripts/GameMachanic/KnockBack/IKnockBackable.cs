using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockBackable
{
    float KnockBackForce {get; set;}
    void ApplyKnockBack(Vector2 direction, float force);
}
