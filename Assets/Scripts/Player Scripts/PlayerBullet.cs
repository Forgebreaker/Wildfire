using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(this.gameObject.transform.InverseTransformDirection(transform.right * MoveSpeed * Time.deltaTime));
        Destroy(gameObject, 2.5f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Ground" || collision.tag == "Player Bullet")
        {
           PlayerHealth EnemyHealth = collision.GetComponent<PlayerHealth>();

           if (EnemyHealth != null)
           {
                EnemyHealth.TakeDamage(1);
                Destroy(this.gameObject);
           }

            Destroy(this.gameObject);
        }


    }

}
