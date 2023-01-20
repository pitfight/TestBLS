using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Aircraft, IObjectContralable<Aircraft>
{
    private void Start()
    {
        Setup(3);
    }

    public override void Setup(int live)
    {
        this.live = live;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void TakeDamage()
    {
        live--;
        if (live == 0) Dead();
        else OnHit?.Invoke();
    }

    public override void Dead()
    {
        OnDead?.Invoke();
    }

    public void Move(Vector2 direction)
    {
        if (controlSwitch == ControlSwitch.On)
        {
            rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime);
            animator.SetFloat("Heel", rb.angularVelocity);
            Debug.Log(rb.angularVelocity);
        }
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }

    public Aircraft GetValue()
    {
        return this;
    }
}
