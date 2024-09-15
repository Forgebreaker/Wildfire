using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement System")]
        [SerializeField] private Rigidbody2D PlayerRB;
        [SerializeField] private float MoveSpeed, JumpForce;
        private float PlayerVelocity_X;

    [Header("Jumping System")]
        [SerializeField] private GameObject GroundCheckPoint;
        [SerializeField] private LayerMask WhatIsGround;
        [SerializeField] private float CheckRadius;
        private bool IsGrounded;
        private bool Abled2DoubleJump;

    [Header("Animation System")]
        private Animator AnimationController;
        [SerializeField] public GameObject RankIcon;

    [Header("Attack System")]
        [SerializeField] private GameObject AttackPoint;
        [SerializeField] private float AttackCoolDown;
        [SerializeField] private GameObject Bullet;
        private float CurrentAttackCoolDown = 0;

    [Header("Dash System")]
        [SerializeField] private float DashTime;
        [SerializeField] private float DashCoolDown;
        [SerializeField] private GameObject DashEffect;
        private SpriteRenderer PlayerAppearance;
        private float CurrentDashTime = 0;
        private float CurrentDashCoolDown = 0;

    void Start()
    {
        AnimationController = this.gameObject.GetComponent<Animator>();
        PlayerAppearance = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMovement();

        RankIcon.transform.rotation = Quaternion.Euler(0,0,0);

        IsGrounded = Physics2D.OverlapCircle(GroundCheckPoint.transform.position, CheckRadius, WhatIsGround);
        
        if (IsGrounded == true) 
        {
            Abled2DoubleJump = true;
        }

        CurrentDashTime -= Time.deltaTime;

        if (CurrentDashTime < 0) 
        { 
            CurrentDashTime = 0;
        }

        CurrentDashCoolDown -= Time.deltaTime;

        if (CurrentDashCoolDown < 0) 
        {
            CurrentDashCoolDown = 0;
        }

        CurrentAttackCoolDown -= Time.deltaTime;

        if (CurrentAttackCoolDown < 0) {
            CurrentAttackCoolDown = 0;
        } 
        else if (CurrentAttackCoolDown > 0) 
        {
            PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y);
        }

        if (CurrentDashTime > 0 && Mathf.Abs(PlayerVelocity_X) > 0)
        {
            PlayerRB.velocity = new Vector2(PlayerVelocity_X * 3 * MoveSpeed, 0);
            GameObject Effect = Instantiate(DashEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
            SpriteRenderer Shadow = Effect.GetComponent<SpriteRenderer>();
            Shadow.sprite = PlayerAppearance.sprite;
            Shadow.color = new Color(1, 1, 1, 0.05f);
        }
        
    }

    public void DashSystem(InputAction.CallbackContext context) 
    {
        if (context.started && CurrentDashCoolDown <= 0) 
        {
            CurrentDashTime = DashTime;
            CurrentDashCoolDown = DashCoolDown;
        }
    }
    private void PlayerMovement()
    {
        // Movement
        PlayerRB.velocity = new Vector2(PlayerVelocity_X * MoveSpeed, PlayerRB.velocity.y);

        AnimationController.SetBool("IsGrounded", IsGrounded);
        AnimationController.SetFloat("Horizontal Input", Mathf.Abs(PlayerVelocity_X));
        AnimationController.SetFloat("Y Velocity", PlayerRB.velocity.y);

        if (PlayerVelocity_X > 0.1 && CurrentAttackCoolDown == 0) 
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (PlayerVelocity_X < -0.1 && CurrentAttackCoolDown == 0)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    public void NewInputSystem_PlayerMovement(InputAction.CallbackContext context) // context is the value that store user input
    {
        // Explain code for you guys to understand
        // As you can see there are Player1 and Player2 Input controllers in the unity editor
        // Inside that I already set WASD Vector2 Digital normalize which will gather user input as a Vector2
        // x axis is equal to A and D or Left and Right buttons (-1 to 1 just like Input.GetAxisRaw in old input system)

        PlayerVelocity_X = context.ReadValue<Vector2>().x;

        
    }

    public void NewInputSystem_PlayerJump(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded == true && CurrentDashTime <= 0 ) // <=> This kinda same with Input.GetButtonDown, which only active when you start press some button
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, JumpForce);
        }

        if (context.started && IsGrounded == false && Abled2DoubleJump == true && CurrentDashTime <= 0)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, JumpForce);
            Abled2DoubleJump = false;
        }
    }

    public void NewInputSystem_PlayerAttack(InputAction.CallbackContext context)
    {
        if (context.started && CurrentAttackCoolDown <= 0) 
        {
            AnimationController.SetTrigger("Attack");
            Instantiate(Bullet, AttackPoint.transform.position, AttackPoint.transform.rotation);
            CurrentAttackCoolDown = AttackCoolDown;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundCheckPoint.transform.position, CheckRadius);
    }

}