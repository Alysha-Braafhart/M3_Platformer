using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player : MonoBehaviour
{
    public int coins;
    public int health = 100;
    public float moveSpeed  = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Image healthImage;
    public AudioClip jumpClip;
    public AudioClip hurtClip;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    public int extraJumpsValue = 1;
    private int extrajumps;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
       extrajumps = extraJumpsValue;
       spriteRenderer = GetComponent<SpriteRenderer>();
       audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isGrounded)
        {
            extrajumps = extraJumpsValue;
        }
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput*moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                PlaySFX(jumpClip);
            }
            else if(extrajumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extrajumps--;
                PlaySFX(jumpClip);
            }
        }
        SetAnimation(moveInput);
        healthImage.fillAmount = health / 100f;
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0)
            {
                animator.Play("Player_Idle");
            }
            else
            {
                animator.Play("Player_Run");
            }
        }
        else
        {
            if(rb.linearVelocityY > 0)
        {
            animator.Play("Player_Jump");
        }
        else
        {
            animator.Play("Player_Fall");
        }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            PlaySFX(hurtClip);
            health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if(health <= 0)
            {
                Die();
            }
        }

         if (collision.gameObject.tag == "InstantDeath")
         {
            health -= 100;
             Die();
         }
       
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void PlaySFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
