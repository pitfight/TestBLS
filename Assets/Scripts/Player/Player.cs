using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Aircraft, IObjectContralable<Player, float>
{
    private void Awake()
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
        //Setup(3);
        OnDead?.Invoke();
        Destroy(gameObject);
    }

    public void Move(Vector2 direction, float input)
    {
        if (controlSwitch == ControlSwitch.On)
        {
            rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime);
            animator.SetFloat("Heel", input);
        }
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }

    public int GetLives() => live;

    public Player GetValue()
    {
        return this;
    }
}
