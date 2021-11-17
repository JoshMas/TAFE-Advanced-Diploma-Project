using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    public Rigidbody2D Rigid => rigid;

    private Animator animator;
    public Animator Animator => animator;

    [SerializeField] private float health = 10;
    [SerializeField] private EnemyState currentState;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.OnFixedUpdate(this);
    }

    public void ChangeState(EnemyState _newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = _newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
