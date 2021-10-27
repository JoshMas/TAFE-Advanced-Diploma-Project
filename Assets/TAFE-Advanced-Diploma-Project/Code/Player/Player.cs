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

    
    [SerializeField] private EnergyBar energy;
    private bool isCharging = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        energy.Initialise();
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
        rigid.AddForce(jumpStrength * Vector2.up, ForceMode2D.Impulse);
    }

    private void OnCrouch(InputValue _value)
    {
        transform.localScale = new Vector3(1, _value.isPressed ? .5f : 1, 1);
        isCharging = _value.isPressed;
        walkSpeed *= _value.isPressed ? .5f : 2;
    }

    private void OnWalk(InputValue _value)
    {
        walkValue = _value.Get<float>();
    }
}
