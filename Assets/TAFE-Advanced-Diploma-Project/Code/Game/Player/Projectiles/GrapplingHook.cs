using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D), typeof(LineRenderer))]
public class GrapplingHook : BasicProjectile
{
    private DistanceJoint2D distanceJoint;
    private LineRenderer line;

    protected override void OnAwake()
    {
        distanceJoint = GetComponent<DistanceJoint2D>();

        distanceJoint.connectedBody = player.Rigid;
        distanceJoint.enabled = false;
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    protected override void OnUpdate()
    {
        RenderLine();
    }

    private void RenderLine()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, player.transform.position);
    }

    public override void Impact()
    {
        distanceJoint.enabled = true;
        distanceJoint.attachedRigidbody.isKinematic = true;
    }

    public override void Activate()
    {
        distanceJoint.enabled = false;
    }
}
