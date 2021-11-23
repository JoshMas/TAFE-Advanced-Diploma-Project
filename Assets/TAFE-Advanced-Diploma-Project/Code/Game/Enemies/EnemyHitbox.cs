using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float time = 3;
    [SerializeField] private Vector2 knockback = Vector2.zero;
    [SerializeField] private bool parryable = true;

    private Enemy enemy;

    private void Start()
    {
        if(transform.parent != null)
        {
            enemy = GetComponentInParent<Enemy>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();

        if (!parryable)
        {
            player.TakeDamage(damage, time, new Vector2(knockback.x * transform.right.normalized.x, knockback.y));
            return;
        }

        if (!player.Parrying)
        {
            player.TakeDamage(damage, time, new Vector2(knockback.x * transform.right.normalized.x, knockback.y));
        }
        else
        {
            player.Parry(damage);
            enemy.Parried();
        }
    }
}
