using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image healthBar;
    public int maxHealth = 100;
    public int health = 100;
    public float speed;
    public float jumpForce;
    private bool IsJumPing;
    private bool doubleJump;

    public ParticleSystem dashParticles;

    public AudioSource SomPulo;
    public AudioSource SomAtack;
    public AudioSource SomR;
    public AudioSource SomP;

    private bool isDashing = false;
    private bool isInvulnerable = false;
    private float dashDuration = 0.5f;
    public float dashDistance = 2.0f;
    private float lastDashTime = -9999.0f;
    private float dashCooldown = 3.0f;
    private bool isDashActive = false;
    private float dashCooldownTimer = 0f;
    private bool canDash = true;

    private bool isFiring;
    private bool canFire = true;

    private Rigidbody2D RIG;
    private Animator AN;
    private float M;
    public Vector3 PosInicial;
    public GameObject ataqueObject;

    void Start()
    {
        RIG = GetComponent<Rigidbody2D>();
        AN = GetComponent<Animator>();
        health = maxHealth;
        PosInicial = transform.position;
    }

    void Update()
    {
        if (!isDashing)
        {
            if (!isFiring)
            {
                Attack();
            }

            Jump();
            Move();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        M = Input.GetAxis("Horizontal");

        RIG.velocity = new Vector2(M * speed, RIG.velocity.y);

        if (M > 0)
        {
            if (!IsJumPing)
            {
                if (AN.GetInteger("transition") != 1)
                {
                    AN.SetInteger("transition", 1);
                    SomR.Play();
                }
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    
        if (M < 0)
        {
            if (!IsJumPing)
            {
                if (AN.GetInteger("transition") != 1)
                {
                    AN.SetInteger("transition", 1);
                    SomR.Play();
                }
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (M == 0 && !IsJumPing && !isFiring)
        {
            if (AN.GetInteger("transition") != 0)
            {
                AN.SetInteger("transition", 0);
                SomR.Stop();
            }
        }
    }



    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !IsJumPing)
        {
            AN.SetInteger("transition", 2); // Define a animação de pulo
            RIG.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            IsJumPing = true;
            SomPulo.Play();
            
            
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.B) && !isFiring)
        {
            StartCoroutine(AttackAnimation());
        }
    }

    IEnumerator AttackAnimation()
    {
        isFiring = true;
        AN.SetInteger("transition", 3); // Definir a animação de ataque

        yield return new WaitForSeconds(0.1f);

        SomAtack.Play(); // Tocar som de ataque

        // Ativar o objeto de ataque
        yield return new WaitForSeconds(0.3f);
        ataqueObject.SetActive(true);

        yield return new WaitForSeconds(0.1f); // Tempo da animação de ataque

        // Desativar o objeto de ataque
        ataqueObject.SetActive(false);

        isFiring = false;
        AN.SetInteger("transition", 7); 
    }


    /*public void Damage(int damageAmount)
    {
        health -= damageAmount;
        AN.SetTrigger("hit");

        if (transform.rotation.y == 0)
        {
            transform.position += new Vector3(-0.5f, 0, 0);
        }

        if (transform.rotation.y == 180)
        {
            transform.position += new Vector3(0.5f, 0, 0);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            SomP.Stop();
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            IsJumPing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D CL)
    {
        if (CL.gameObject.tag == "queda")
        {
            health -= 1;
            transform.position = PosInicial;
        }
    }
}
