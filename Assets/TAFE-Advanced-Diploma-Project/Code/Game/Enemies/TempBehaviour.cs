using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBehaviour : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform player;
    [SerializeField] private float speed = 3;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = player.transform.position - transform.position;
        rigid.velocity = velocity.normalized * speed;
    }
}
