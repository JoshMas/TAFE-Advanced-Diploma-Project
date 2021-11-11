using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float time = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();
        if (!player.Parrying)
        {
            player.TakeDamage(damage, time);
        }
        else
        {
            player.Parry(damage);
            //Add the part where the enemy/projectile gets parried
        }
    }
}
