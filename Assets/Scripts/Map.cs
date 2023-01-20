using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : ObjectBase, IObjectContralable<Map>
{
    public Map GetValue()
    {
        return this;
    }

    public void Move(Vector2 direction)
    {
        if (controlSwitch == ControlSwitch.On)
        {
            rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime);
        }
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }
}
