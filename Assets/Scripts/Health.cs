using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());

            damageDealer.Hit();
        }
    }
    
    private void TakeDamage(int damage)
    {
        if (damage >= health)
        {
            Destroy(gameObject);
        }
        else
        {
            health -= damage;
        }
    }
}
