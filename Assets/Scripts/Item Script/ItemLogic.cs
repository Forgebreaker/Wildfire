using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    [SerializeField] private bool IsHeath;
    [SerializeField] private bool IsInvincible;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 15f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            PlayerHealth Affect2Player = collision.GetComponent<PlayerHealth>();
            if (IsHeath && Affect2Player != null) 
            {
                Affect2Player.HealMode();
                Destroy(gameObject);
            }
            if (IsInvincible && Affect2Player != null)
            { 
                Affect2Player.ActiveGodMode();
                Destroy(gameObject);
            }
        }
    }
}
