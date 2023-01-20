using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : ObjectBase, IObjectContralable<Map, Action<GameObject>>
{
    private const float DESTROY_OF_POSITION = -12f;
    public Map GetValue()
    {
        return this;
    }

    public void Move(Vector2 direction, Action<GameObject> callBeck)
    {
        if (controlSwitch == ControlSwitch.On)
        {
            rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime);
            if (rb.position.x < DESTROY_OF_POSITION) callBeck?.Invoke(gameObject);
        }
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }
}
