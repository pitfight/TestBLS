using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Aircraft, IObjectContralable<Aircraft, Action<GameObject>>
{
    [SerializeField] private GameObject[] variebleAircraft;
    private const float DESTROY_OF_POSITION = -6f;

    private void Start()
    {
        Setup(1);
    }

    public override void Setup(int live)
    {
        this.live = live;
        if (animator != null) animator = GetComponent<Animator>();
        if (rb != null) rb = GetComponent<Rigidbody2D>();
        foreach (var go in variebleAircraft) if (go.activeSelf) go.SetActive(false);
        variebleAircraft[UnityEngine.Random.Range(0, variebleAircraft.Length)].SetActive(true);
    }

    public override void TakeDamage()
    {
        live--;
        if (live <= 0) Dead();
        else OnHit?.Invoke();
    }

    public override void Dead()
    {
        ObjectPool.Recycle(gameObject);
        OnDead?.Invoke();
    }

    public void Spawn(Vector2 position)
    {
        transform.position = position;
        Setup(1);
    }

    public void Move(Vector2 direction, Action<GameObject> callBeck)
    {
        if (controlSwitch == ControlSwitch.On)
        {
            rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime);
            rb.MovePosition(rb.position + direction * velocity * Time.fixedDeltaTime);
            if (rb.position.x < DESTROY_OF_POSITION) callBeck?.Invoke(gameObject);
        }
    }

    public Aircraft GetValue()
    {
        return this;
    }
}
