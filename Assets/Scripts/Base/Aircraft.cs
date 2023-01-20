using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Aircraft : ObjectBase
{
    protected Animator animator;
    protected int live;

    public abstract void Setup(int live);
    public abstract void TakeDamage();
    public abstract void Dead();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage();
    }
}