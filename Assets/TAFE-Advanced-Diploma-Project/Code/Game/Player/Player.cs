using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Vector2 exactSpeedAxis;
    public Vector2 lerpSpeedAxis;

    [SerializeField] private float jumpStrength = 1;
    public float JumpStrength => jumpStrength;

    public Rigidbody2D Rigid { get; private set; }

    public Animator Animator { get; private set; }

    #region grapplingHook
    [SerializeField] private GameObject grappleTemplate;
    public Transform Grapple { get; private set; }
    private LineRenderer grappleLine;
    private DistanceJoint2D grappleSpring;
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
    #endregion

    [SerializeField] private AbilityState currentState;
    [SerializeField] private bool isParrying = false;
    public bool Parrying => isParrying;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        energy.Initialise();

        groundMask = LayerMask.GetMask("Ground");
        if(currentState != null)
            currentState.OnEnter(this);

        Grapple = Instantiate(grappleTemplate).transform;
        grappleLine = Grapple.GetComponent<LineRenderer>();
        grappleSpring = Grapple.GetComponent<DistanceJoint2D>();
        grappleSpring.connectedBody = Rigid;
        grappleSpring.enabled = false;
        Grapple.gameObject.SetActive(false);
    }

    private void Update()
    {
        MoveLerpSpeedAxis();
        currentState.OnUpdate();
        energy.CountDownDenial();
    }

    private void MoveLerpSpeedAxis()
    {
        if((lerpSpeedAxis - exactSpeedAxis).magnitude < 0.001f)
        {
            lerpSpeedAxis = exactSpeedAxis;
        }
        else
        {
            lerpSpeedAxis = Vector2.Lerp(lerpSpeedAxis, exactSpeedAxis, 10 * Time.deltaTime);
        }
        Animator.SetFloat("xSpeed", lerpSpeedAxis.x);
        Animator.SetFloat("ySpeed", lerpSpeedAxis.y);
    }

    private void FixedUpdate()
    {
        currentState.OnFixedUpdate();
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

    private void OnWalk(InputValue _value)
    {
        exactSpeedAxis = _value.Get<Vector2>();
        currentState.OnMove();
        //Debug.Log(_value.Get<float>());
    }

    private void OnAttack()
    {
        currentState.OnAttack();
    }

    private void OnRanged(InputValue _value)
    {
        currentState.OnRanged(_value.isPressed);
    }

    private void OnInstall()
    {
        currentState.OnInstall();
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

    public Vector2 GetMouseDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
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

    public void ReparentGrapple(Transform _newParent)
    {
        grappleSpring.transform.parent = _newParent;
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

    public void Parry(float _amount)
    {
        Energy.Parry(_amount);
        Animator.SetTrigger("Attack");
    }

    private void OnOpenMenu()
    {
        Application.Quit();
    }
}
