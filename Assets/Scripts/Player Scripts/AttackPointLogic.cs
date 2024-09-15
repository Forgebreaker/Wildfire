using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            PlayerHealth EnemyHealth = collision.GetComponent<PlayerHealth>();
            EnemyHealth.TakeDamage(1);
        }
    }
}
