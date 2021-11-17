using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicProjectile : MonoBehaviour
{

    [SerializeField] protected float speed = 1;
    [SerializeField] protected float duration = 1;
    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= duration)
        {
            Detonate();
        }
    }

    public void Detonate()
    {

    }
}
