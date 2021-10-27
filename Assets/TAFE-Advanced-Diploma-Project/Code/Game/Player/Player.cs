using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 1;
    private float walkValue;

    [SerializeField] private float jumpStrength = 1;
    Rigidbody2D rigid;
    LayerMask groundMask;

    
    [SerializeField] private EnergyBar energy;
    private bool isCharging = false;

    private AbilityState currentState;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        energy.Initialise();


        groundMask = LayerMask.GetMask("Ground");


    }

    private void Update()
    {
        rigid.velocity = new Vector2(walkValue * walkSpeed, rigid.velocity.y);
        if (isCharging)
        {
            energy.Charge();
        }
    }

    private void OnJump()
    {
        if(IsGrounded())
            rigid.AddForce(jumpStrength * Vector2.up, ForceMode2D.Impulse);
    }

    private void OnCrouch(InputValue _value)
    {
        isCharging = _value.isPressed;
        transform.localScale = new Vector3(1, isCharging ? .5f : 1, 1);
        walkSpeed *= isCharging ? .5f : 2;
    }

    private void OnWalk(InputValue _value)
    {
        walkValue = _value.Get<float>();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(transform.position, new Vector2(1, .1f), 0, groundMask);
    }
}
