using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.VirtualTexturing;
public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, OnAttackPerformed;
    
    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }
    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }
}
