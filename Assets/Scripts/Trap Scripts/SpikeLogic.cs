using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeLogic : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            PlayerHealth PlayerHP = collision.GetComponent<PlayerHealth>();
            if (PlayerHP)
            {
                PlayerHP.TakeDamage(1);
            }
        }
    }
}
