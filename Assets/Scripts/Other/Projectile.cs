using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ObjectBase , IObjectContralable<Projectile, Action<GameObject>>
{
    private const float DESTROY_OF_POSITION = 6f;
    public Projectile GetValue()
    {
        return this;
    }

    public void Move(Vector2 direction, Action<GameObject> callBeck)
    {
        if (controlSwitch == ControlSwitch.On)
        {
            rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime);
            if (rb.position.x > DESTROY_OF_POSITION) callBeck?.Invoke(gameObject);

        }
    }

    public void SetControl(ControlSwitch controlSwitch)
    {
        this.controlSwitch = controlSwitch;
    }

    public void Spawn(Vector2 position)
    {
        velocity = 45;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit?.Invoke();
        ObjectPool.Recycle(gameObject);
    }
}
