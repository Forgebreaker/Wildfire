using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 10;
    [SerializeField] private int CurrentHealth;
    [SerializeField] private Image HealthUI;
    [SerializeField] private GameObject GodModeSymbol;
    [SerializeField] private float GodModeTime = 5;
    [SerializeField] private float GodModeCurrentTime = 0;
    private SceneManager sceneManager;
    private Slider HealthSlider;

    [Header("TakeDamgeEffect")]
        
        [SerializeField] private float InvincibilityTime;
        private float CurrentInvincibleTime;
        private SpriteRenderer InvincibilityEffect;

    [Header("Dead Effect")]

        private SpriteRenderer Appearance;
        private Collider2D PlayerCollider;
        private bool Exploded = false;
        [SerializeField] private GameObject Explode;
        
    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthSlider = this.gameObject.GetComponent<Slider>();
        InvincibilityEffect = this.gameObject.GetComponent<SpriteRenderer>();
        Appearance = this.gameObject.GetComponent<SpriteRenderer>();
        PlayerCollider = this.gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        HealthSlider.value = CurrentHealth;
        
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        GodModeSymbol.transform.rotation = Quaternion.Euler(0, 0, 0);


        // Godmode

        GodModeCurrentTime -= Time.deltaTime;

        if (GodModeCurrentTime <= 0)
        {
            GodModeSymbol.SetActive(false);
        }
        else
        {
            GodModeSymbol.SetActive(true);
        }


        // Invincibility

        CurrentInvincibleTime -= Time.deltaTime;

        if (CurrentInvincibleTime > 0)
        {
            InvincibilityEffect.color = new Color(1, 1, 1, 0.5f);
        }
        else 
        {
            InvincibilityEffect.color = new Color(1, 1, 1, 1f);
        }

        // Fall to Death
        
        if (this.gameObject.transform.position.y <= -12.5f)
        {
            CurrentHealth = 0;
        }

        // Death 
        if (CurrentHealth == 0)
        {
            if (Exploded == false)
            {
                GameObject Dead = Instantiate(Explode, this.gameObject.transform.position, Quaternion.identity);
                Exploded = true;
            }
            Appearance.enabled = false;
            PlayerCollider.enabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(this.gameObject, 2f);
        }
    }
    public void HealMode()
    {
        CurrentHealth += 3;
    }
    public void ActiveGodMode() 
    {
        GodModeCurrentTime = GodModeTime;
    }

    public void TakeDamage(int Damage) 
    {
        if (CurrentInvincibleTime <= 0 && GodModeCurrentTime <= 0)
        {
            CurrentHealth -= Damage;
            CurrentInvincibleTime = InvincibilityTime;
        }
    }
}
