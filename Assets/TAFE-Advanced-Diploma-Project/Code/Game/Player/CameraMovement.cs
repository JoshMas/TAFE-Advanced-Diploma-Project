using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float maxHorizontalDistance = 4;
    [SerializeField] private float maxVerticalDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = player.transform.position - transform.position;
        if(distance.x > maxHorizontalDistance)
        {
            transform.position = new Vector3(player.transform.position.x - maxHorizontalDistance, transform.position.y, -18);
        }
        else if(-distance.x > maxHorizontalDistance)
        {
            transform.position = new Vector3(player.transform.position.x + maxHorizontalDistance, transform.position.y, -18);
        }

        if(distance.y > maxVerticalDistance)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y - maxVerticalDistance, -18);
        }
        else if(-distance.y > maxVerticalDistance)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + maxVerticalDistance, -18);
        }
    }
}
