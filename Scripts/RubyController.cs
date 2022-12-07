using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;

    public TextMeshProUGUI CogsText;
    public int maxCogs = 4;

    public TextMeshProUGUI LoseText;
    
    public GameObject projectilePrefab;

    
    public Transform DamageEffect;

    public AudioClip hitSound;

    public AudioClip LoseSound;

    public AudioClip WinSound;

    public int health { get { return currentHealth; }} 
    int currentHealth;

    int currentCogs;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();  

        currentHealth = maxHealth;

        currentCogs = maxCogs;

        audioSource = GetComponent<AudioSource>(); 

        DamageEffect.GetComponent<ParticleSystem> ().enableEmission = false;
    }

    IEnumerator atopDamageEffect()
    {
        yield return new WaitForSeconds (.4f);

        DamageEffect.GetComponent<ParticleSystem> ().enableEmission = false;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

         if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x  + speed * horizontal *Time.deltaTime;
        position.y = position.y + speed * vertical *Time.deltaTime;
        
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        animator.SetTrigger("Hit");
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            DamageEffect.GetComponent<ParticleSystem> ().enableEmission = true;
            StartCoroutine (atopDamageEffect ());
            PlaySound(hitSound);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if (amount == 0)
        {
            PlaySound(LoseSound);
        }

        if (amount == 4)
        {
            PlaySound(WinSound);
        }

        if (amount > 1)
        {
            Debug.Log("You Lose");
        }
    }


    public void ChangeScore(int amount)
    {

    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }

}
