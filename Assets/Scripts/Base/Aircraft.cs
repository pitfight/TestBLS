using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Aircraft<T> : MonoBehaviour
{
    private T Value;

    protected Animator animator;
    protected Rigidbody2D rb;
    protected int live;
    protected AircraftControlType controlType;
    protected ControlSwitch controlSwitch;

    public Action OnDead;
    public Action OnTakeDamage;

    public abstract void Setup(AircraftControlType controlType, int live);
    public abstract void TakeDamage();
    public abstract void Dead();

    public T GetValue()
    {
        return Value;
    }

    public void SetValue(T value)
    {
        Value = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage();
    }
}