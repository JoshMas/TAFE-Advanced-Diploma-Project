using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 1;
    public float WalkSpeed => walkSpeed;
    public float currentSpeed;

    [SerializeField] private float jumpStrength = 1;
    public float JumpStrength => jumpStrength;

    public Rigidbody2D Rigid { get; private set; }
    private LayerMask groundMask;

    
    [SerializeField] private EnergyBar energy;
    public EnergyBar Energy => energy;
    
    private bool isCharging = false;

    [SerializeField] private AbilityState currentState;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        energy.Initialise();


        groundMask = LayerMask.GetMask("Ground");
        if(currentState != null)
            currentState.OnEnter(this);
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    public void ChangeState(AbilityState _newState)
    {
        if(currentState != null)
        {
            currentState.OnExit();
        }
        currentState = _newState;
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    private void OnJump()
    {
        currentState.OnJump();
    }

    private void OnCrouch(InputValue _value)
    {
        currentState.OnCrouch(_value.isPressed);
    }

    public void Crouch(bool _isPressed)
    {
        isCharging = _isPressed;
        transform.localScale = new Vector3(1, isCharging ? .5f : 1, 1);
        walkSpeed *= isCharging ? .5f : 2;
    }

    private void OnWalk(InputValue _value)
    {
        currentState.OnWalk(_value.Get<float>());
    }

    private void OnAbilityOne()
    {
        currentState.OnAbilityOne();
    }

    private void OnAbilityTwo()
    {
        currentState.OnAbilityTwo();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(transform.position, new Vector2(1, .1f), 0, groundMask);
    }

    public void Charge()
    {
        if (isCharging)
        {
            energy.Charge();
        }
    }

    private void OnOpenMenu()
    {

    }
}
