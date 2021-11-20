using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    public Vector2 exactSpeedAxis;
    public Vector2 lerpSpeedAxis;

    [SerializeField] private float jumpStrength = 1;
    public float JumpStrength => jumpStrength;

    private Rigidbody2D rigid;
    public Rigidbody2D Rigid => rigid;

    private Animator animator;
    public Animator Animator => animator;

    #region grapplingHook
    [SerializeField] private GameObject grappleTemplate;
    private Transform grapple;
    public Transform Grapple => grapple;
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

    [SerializeField] private EnergyBar health;
    public EnergyBar Health => health;

    [SerializeField] private AbilityState currentState;
    [SerializeField] private bool isParrying = false;
    public bool Parrying => isParrying;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        energy.Initialise();

        groundMask = LayerMask.GetMask("Ground");
        if(currentState != null)
            currentState.OnEnter(this);

        grapple = Instantiate(grappleTemplate).transform;
        grappleLine = grapple.GetComponent<LineRenderer>();
        grappleSpring = grapple.GetComponent<DistanceJoint2D>();
        grappleSpring.connectedBody = Rigid;
        grappleSpring.enabled = false;
        grapple.gameObject.SetActive(false);
        health.Initialise();
        health.Charge(100);
    }

    private void Update()
    {
        MoveLerpSpeedAxis();
        currentState.OnUpdate();
        energy.CountDownDenial();
        health.CountDownDenial();
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
        animator.SetFloat("xSpeed", lerpSpeedAxis.x);
        animator.SetFloat("ySpeed", lerpSpeedAxis.y);
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

    private void OnParry()
    {
        currentState.OnParry();
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
        Vector3 mouse = Mouse.current.position.ReadValue();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse + Vector3.forward * 18);
        return (mousePos - transform.position).normalized;
    }

    public void GrappleActive(bool _isActive)
    {
        grapple.gameObject.SetActive(_isActive);
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
        grapple.position = _position;
        grappleLine.SetPosition(0, transform.position);
        grappleLine.SetPosition(1, grapple.position);
    }

    public void TakeDamage(float _damage, float _time, Vector2 _knockback)
    {
        if(health.TakeDamage(_damage, _time))
        {
            //Game Over here
            Debug.Log("Game Over");
            animator.SetTrigger("Dead");
        }
        else
        {
            currentState.ChangeState(typeof(StunnedState));
            rigid.AddForce(_knockback, ForceMode2D.Impulse);
        }
    }

    public void Parry(float _amount)
    {
        health.Charge(_amount, true);
        animator.SetTrigger("Attack");
    }

    private void OnOpenMenu()
    {
        Application.Quit();
    }
}
