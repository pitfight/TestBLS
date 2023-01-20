using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class ObjectBase : MonoBehaviour
{
    [SerializeField] protected float velocity = 1f;
    [SerializeField] protected Rigidbody2D rb;
    protected ControlSwitch controlSwitch = ControlSwitch.Off;

    public Action OnHit;
    public Action OnDead;

    private void OnEnable()
    {
        controlSwitch = ControlSwitch.On;
    }

    private void OnDisable()
    {
        controlSwitch = ControlSwitch.Off;
    }
}
