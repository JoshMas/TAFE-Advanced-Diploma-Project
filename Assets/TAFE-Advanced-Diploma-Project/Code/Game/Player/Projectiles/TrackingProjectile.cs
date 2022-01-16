using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : BasicProjectile
{
    [SerializeField] private float acceleration = 1;

    protected override void OnUpdate()
    {
        
    }

    protected override void OnFixedUpdate()
    {
        rigid.velocity += (GetMousePosition() - (Vector2)transform.position).normalized * acceleration;
    }
}
