using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class CharacterA : MonoBehaviour
{
    public float moveSpeed;
    private float xInput, yInput;
    public Rigidbody2D rb2d;
    private SpriteRenderer sp;
    public float jumpForce;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask iceLayer;
    public bool isIce;
    public Vector2 oldVelocity;

    public int maxHealth = 1000;
    public int currentHealth;
    private bool isEating = false;
    public HealthBar healthBar;
    public int coef = 1;
    private int tempMaxHealth;
    public int satiety;

    // Добавил этот блок аудио
    public AudioSource jump;
    public AudioSource runOnGrass;
    public AudioSource eatingFood;
    public AudioSource teleportation;
    public AudioSource runOnIce;
    public AudioSource firstSong;
    public AudioSource secondSong;
    //public AudioSource thirdSong;
    //public AudioSource fourthSong;
    //public AudioSource fifthSong;
    private bool isSecondSong;

    private bool canDoubleJump;
    

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        sp = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        tempMaxHealth = maxHealth;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        firstSong.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded||isIce)
            {
                Jump();
                canDoubleJump = true;
            }
            
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // Добавил эти два условия на запуск звуков хождения по земле и льду (лёд не проверял, так как нет у меня льда, но должно работать. Лучше проверь, как будет возможность)
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && isGrounded)
        {
            if (!runOnGrass.isPlaying)
                runOnGrass.Play();
        }
        else runOnGrass.Stop();
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && isIce)
        {
            if (!runOnIce.isPlaying)
                runOnIce.Play();
        }
        else runOnIce.Stop();
        // Конец добавленного блока

        xInput = Input.GetKey(KeyCode.LeftArrow) ? Input.GetAxis("Horizontal") :
            Input.GetKey(KeyCode.RightArrow) ? Input.GetAxis("Horizontal") : 0;

        transform.Translate(xInput * moveSpeed, yInput * moveSpeed, 0);
        
        isIce = Physics2D.OverlapCircle(groundCheck.position, 0.4f, iceLayer); // Изменил с 0.2 на 0.4, для личного удобства, можешь изменить под свою игру 
        if (isIce && xInput == 0)
        {
            rb2d.AddForce(oldVelocity);
            oldVelocity = new Vector2(moveSpeed * xInput, rb2d.velocity.y);
            //rb2d.velocity = new Vector2(oldVelocity.x/2, rb2d.velocity.y);

        }
        
        else if (isIce && xInput != 0)
        {
            rb2d.velocity = new Vector2(moveSpeed * xInput, rb2d.velocity.y)*10;
            oldVelocity = rb2d.velocity;
            FlipPlayer();

        }

        else
        {
            PlatformerMove();
            FlipPlayer();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);  // Изменил с 0.2 на 0.4, для личного удобства, можешь изменить под свою игру 

        // Замена одной мелодии на другую по её окончании
        //if (!isSecondSong && !secondSong.isPlaying && !firstSong.isPlaying)
        //{
        //    secondSong.Stop();
        //    firstSong.Play();
        //    isSecondSong = !isSecondSong;
        //}
        //if (isSecondSong && !secondSong.isPlaying && !firstSong.isPlaying)
        //{
        //    firstSong.Stop();
        //    secondSong.Play();
        //    isSecondSong = !isSecondSong;
        //}
            //if (coef == 1 && !firstSong.isPlaying) firstSong.Play();
            //if (coef == 2 && !secondSong.isPlaying)
            //{
            //    firstSong.Stop();
            //    secondSong.Play();
            //}
            //if (coef == 3) secondSong.Stop();
            //if (coef >= 3 && !isFourthSong && !thirdSong.isPlaying && !fourthSong.isPlaying)
            //{
            //    thirdSong.Play();
            //    isFourthSong = !isFourthSong;
            //}
            //if (coef >= 3 && isFourthSong && !thirdSong.isPlaying && !fourthSong.isPlaying)
            //{
            //    fourthSong.Play();
            //    isFourthSong = !isFourthSong;
            //}
        TakeDamage();
    }

    public void TakeDamage()
    {
        if (isEating) return;

        if (tempMaxHealth <= 0 && coef < 5)
        {
            coef++;
            tempMaxHealth = maxHealth * coef;
        }
        currentHealth -= coef;
        if (currentHealth <= 0) Destroy(gameObject);
        if (coef <= 5) tempMaxHealth -= coef;
        healthBar.SetHealth(currentHealth);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            eatingFood.Play(); // Добавил проигрывание поедания еды
            isEating = true;
            Eating();
            isEating = false;
        }

        else if (collision.gameObject.tag == "Teleport")
        {
            teleportation.Play(); // Добавил проигрывание телепортирования
        }
    }

    public void Eating()
    {
        if (currentHealth + satiety > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(maxHealth);
        }
        else
        {
            currentHealth += satiety;
            healthBar.SetHealth(currentHealth);
        }
    }


    void PlatformerMove()
    {
        rb2d.velocity = new Vector2(moveSpeed * xInput, rb2d.velocity.y);
        oldVelocity = rb2d.velocity;
    }

    void FlipPlayer()
    {
        if (rb2d.velocity.x < 0f)
        {
            sp.flipX = true;
        }

        else if(rb2d.velocity.x > 0f)
        {
            sp.flipX = false;
        }
    }

    void Jump()
    {
        jump.Play(); // Добавил проигрывание прыжка
        rb2d.velocity = Vector2.up * jumpForce;
    }
}
