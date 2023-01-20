using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Aircraft, IObjectContralable<Aircraft>
{
    [SerializeField] private SpriteRenderer sRenderer;

    private void Start()
    {
        Setup(1);
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

    public void Spawn(Vector2 position)
    {
        transform.position = position;
    }

    public void Move(Vector2 direction)
    {
        if (controlSwitch == ControlSwitch.On)
        {
            rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime);
        }
    }

    public Aircraft GetValue()
    {
        return this;
    }
}
