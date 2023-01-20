using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Aircraft, IObjectContralable
{
    private void Awake()
    {
        Setup(3);
    }

    private void OnEnable()
    {
        SetControl(ControlSwitch.On);
    }

    public override void Setup(int live)
    {
        this.live = live;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        SetControl(ControlSwitch.Off);
    }

    public override void TakeDamage()
    {
        live--;
        if (live == 0) Dead();
        else OnTakeDamage?.Invoke();
    }

    public override void Dead()
    {
        OnDead?.Invoke();
    }

    public void Move(Vector2 direction)
    {
        if (controlSwitch == ControlSwitch.On)
        {

        }
    }

    public void Spawn(Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    public void SetControl(ControlSwitch controlSwitch)
    {
        this.controlSwitch = controlSwitch;
    }
}
