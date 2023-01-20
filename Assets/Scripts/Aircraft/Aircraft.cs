using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aircraft : MonoBehaviour
{
    [SerializeField] private float objectSpeed;
    public Action OnDead;

    private void OnEnable()
    {
        OnDead += Dead;
    }

    public abstract void Dead();
}
