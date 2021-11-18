using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private float damage;
    public float Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<Enemy>().TakeDamage(damage);
    }
}
