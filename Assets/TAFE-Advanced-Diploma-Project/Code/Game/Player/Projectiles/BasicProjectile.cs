using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicProjectile : MonoBehaviour
{

    [SerializeField] protected float speed = 1;
    [SerializeField] protected float duration = 1;
    private float timer = 0;

    private Player player;
    protected Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(duration == -1)
        {
            return;
        }
        if(timer >= duration)
        {
            Activate();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        FixedMove();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Impact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Impact();
    }
    
    protected virtual void OnStart()
    {

    }

    protected virtual void Move()
    {

    }

    protected virtual void FixedMove()
    {
        
    }

    public virtual void Activate()
    {

    }

    public virtual void Impact()
    {

    }

    protected Vector2 GetMousePosition()
    {
        Vector3 mouse = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(mouse + Vector3.forward * -Camera.main.transform.position.z);
    }

}
