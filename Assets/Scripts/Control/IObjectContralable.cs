using UnityEngine;

public interface IObjectContralable
{
    void Move(Vector2 direction);
    void Spawn(Vector2 position);
    void SetControl(ControlSwitch controlSwitch);
}
