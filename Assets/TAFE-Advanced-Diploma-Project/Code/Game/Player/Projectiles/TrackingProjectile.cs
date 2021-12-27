using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrackingProjectile : BasicProjectile
{
    [SerializeField] private float acceleration = 1;

    protected override void OnStart()
    {
        rigid.velocity = (GetMousePosition() - (Vector2)transform.position).normalized * speed;
    }

    protected override void FixedMove()
    {
        rigid.velocity += (GetMousePosition() - (Vector2)transform.position).normalized * acceleration;
    }
}
