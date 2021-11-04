using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 1;
    public float WalkSpeed => walkSpeed * walkSpeedMultiplier;
    public float targetSpeedAxis;
    private float walkSpeedMultiplier = 1;

    [SerializeField] private float jumpStrength = 1;
    public float JumpStrength => jumpStrength;

    public Rigidbody2D Rigid { get; private set; }

    #region grapplingHook
    [SerializeField] private GameObject grappleTemplate;
    public Transform Grapple { get; private set; }
    private LineRenderer grappleLine;
    private SpringJoint2D grappleSpring;
    #endregion

    #region collisionChecks
    private LayerMask groundMask;
    public LayerMask GroundMask => groundMask;

    private Vector2 groundCheckBounds = new Vector2(.9f, .1f);
    private Vector2 wallCheckBounds = new Vector2(.1f, 1.6f);
    #endregion

    #region energybar
    [SerializeField] private EnergyBar energy;
    public EnergyBar Energy => energy;
    
    private bool isCharging = false;

    [SerializeField] private float passiveChargeRate = 1;
    #endregion

    [SerializeField] private AbilityState currentState;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        energy.Initialise();

        groundMask = LayerMask.GetMask("Ground");
        if(currentState != null)
            currentState.OnEnter(this);

        Grapple = Instantiate(grappleTemplate).transform;
        grappleLine = Grapple.GetComponent<LineRenderer>();
        grappleSpring = Grapple.GetComponent<SpringJoint2D>();
        grappleSpring.connectedBody = Rigid;
        grappleSpring.enabled = false;
        Grapple.gameObject.SetActive(false);
    }

    private void Update()
    {
        currentState.OnUpdate();
        energy.CountDownDenial();
        energy.PassiveGain(passiveChargeRate);
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
        walkSpeedMultiplier = isCharging ? .5f : 1;
    }

    private void OnWalk(InputValue _value)
    {
        currentState.OnMove(_value.Get<Vector2>());
        //Debug.Log(_value.Get<float>());
    }

    private void OnAbilityOne()
    {
        currentState.OnAbilityOne();
    }

    private void OnAbilityTwo(InputValue _value)
    {
        currentState.OnAbilityTwo(_value.isPressed);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(transform.position, groundCheckBounds, 0, groundMask);
    }

    public bool IsWallClinging()
    {
        return !IsGrounded() && Physics2D.OverlapBox(transform.position + transform.up + .5f * transform.right, wallCheckBounds, 0, groundMask);
    }

    public bool IsStillWallClinging()
    {
        return !IsGrounded() && Physics2D.OverlapBox(transform.position + transform.up - .5f * transform.right, wallCheckBounds, 0, groundMask);
    }

    public void Charge()
    {
        if (isCharging)
        {
            energy.Charge();
        }
    }

    public Vector2 GetMouseDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Debug.Log(mousePos);
        return (mousePos - transform.position).normalized;
    }

    public void GrappleActive(bool _isActive)
    {
        Grapple.gameObject.SetActive(_isActive);
    }

    public void GrappleAttach(bool _isAttached)
    {
        grappleSpring.enabled = _isAttached;
    }

    public void UpdateGrapplePosition(Vector2 _position)
    {
        Grapple.position = _position;
        grappleLine.SetPosition(0, transform.position);
        grappleLine.SetPosition(1, Grapple.position);
    }

    public void TakeDamage(float _damage, float _time)
    {
        if(energy.TakeDamage(_damage, _time))
        {
            //Game Over here
            Debug.Log("Game Over");
        }
    }

    private void OnOpenMenu()
    {
        Application.Quit();
    }
}
