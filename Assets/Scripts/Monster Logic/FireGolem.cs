using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGolem : MonoBehaviour
{
    private Animator AnimationController;
    private float ShootCountDown;
    private bool Attack;
    // Start is called before the first frame update
    void Start()
    {
        ShootCountDown = 3f;
        AnimationController = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootCountDown -= Time.deltaTime;

        if (ShootCountDown < 0)
        {
            Attack = true;
            ShootCountDown = 7.5f;
        } else 
        {
            Attack = false; 
        }
        AnimationController.SetBool("Attack", Attack);
    }


}
