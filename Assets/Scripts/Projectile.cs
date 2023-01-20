using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ObjectBase , IObjectContralable<Projectile>
{
    public Projectile GetValue()
    {
        return this;
    }

    public void Move(Vector2 direction)
    {
        if (controlSwitch == ControlSwitch.On)
        {
            rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
        }
    }

    public void SetControl(ControlSwitch controlSwitch)
    {
        this.controlSwitch = controlSwitch;
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHit?.Invoke();
    }
}
