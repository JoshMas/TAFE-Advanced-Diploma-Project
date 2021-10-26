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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigid.velocity = new Vector2(walkValue * walkSpeed, rigid.velocity.y);
    }

    private void OnJump()
    {
        rigid.AddForce(jumpStrength * Vector2.up, ForceMode2D.Impulse);
    }

    private void OnCrouch(InputValue _value)
    {
        transform.localScale = new Vector3(1, _value.isPressed ? .5f : 1, 1);
    }

    private void OnWalk(InputValue _value)
    {
        walkValue = _value.Get<float>();
    }
}
