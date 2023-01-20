using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class ObjectBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected ControlSwitch controlSwitch;
}
