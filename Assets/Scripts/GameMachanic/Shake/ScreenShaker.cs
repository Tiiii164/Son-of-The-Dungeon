using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    [SerializeField] private float _shakeForce = 1f;

    private CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(Vector2 direction)
    {
        _impulseSource.GenerateImpulseWithVelocity(-direction * _shakeForce);
    }
}