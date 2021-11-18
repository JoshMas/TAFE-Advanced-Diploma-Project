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

    [SerializeField] private float maxHealth = 10;
    private float health;
    [SerializeField] private EnemyState currentState;

    private Vector2 originalPosition;
    public Vector2 OriginalPosition => originalPosition;

    private Transform playerTransform;
    private LayerMask groundMask;


    private float timer = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
        health = maxHealth;
        groundMask = LayerMask.GetMask("Ground");
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState.OnEnter(this);
        playerTransform = GameObject.FindWithTag("Player").transform;
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
        timer = 0;
    }

    public bool KeepTime(float _time, float _duration)
    {
        timer += _time;
        return timer >= _duration;
    }

    public void TakeDamage(float _amount)
    {
        health -= _amount;
        if(health <= 0)
        {
            Die();
        }
    }

    public bool PlayerInLineOfSight(float _distance)
    {
        if(Vector2.Distance(transform.position, playerTransform.position) > _distance)
        {
            return false;
        }
        else
        {
            return Physics2D.Raycast(transform.position, GetDirectionToPlayer(), _distance, groundMask);
        }
    }

    public Vector2 GetDirectionToPlayer()
    {
        return (playerTransform.position + Vector3.up - transform.position).normalized;
    }

    private void Die()
    {
        TempSpawner.Instance.Spawn();
        Destroy(gameObject);
    }
}
