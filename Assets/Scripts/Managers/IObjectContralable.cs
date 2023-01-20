using System;
using UnityEngine;

public interface IObjectContralable<T1,T2>
{
    void Move(Vector2 direction, T2 args);
    void Spawn(Vector2 position);
    T1 GetValue();
}
