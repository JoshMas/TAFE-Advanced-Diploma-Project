using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float time = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<Player>().TakeDamage(damage, time);
    }
}
