using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Player : MonoBehaviour
{
        public Image healthBar;
        public int maxHealth = 100; 
        public int health = 5;
        public float speed;
        private float M;
        public float jumpForce;
        private bool IsJumPing;
        private bool DoubleJump;
    
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
        
        
        //public Text dashCooldownText;
        //public Text shieldCooldownText;
    
        
        private float shieldDuration = 3.0f;
        private float shieldCooldown = 20.0f;
        private float lastShieldActivationTime = -9999.0f; 
        private bool isShieldActive = false;
        private GameObject shieldObject;
        public Sprite shieldSprite;
        
        
        public GameObject power;
        public Transform spawn;
        private bool isfire;
        private bool canFire = true;
        
        public bool stage2 = false;
        public bool stage3 = false;
        
        private Rigidbody2D RIG;
        private Animator AN;
        
        // Start is called before the first frame update
        void Start()
        {
            RIG = GetComponent<Rigidbody2D>();
    
            AN = GetComponent<Animator>();
            
            shieldObject = null;
            
            health = maxHealth;
        }
    
        // Update is called once per frame
        void Update()
        {
            Jump();
    
            if (!isDashing)
            {
                Move();
                AT();
                //Dash();
                
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ToggleShield(); 
                }
            }
            
            /*if (isDashActive)
            {
                dashCooldownText.text = "TP Ativo";
            }
            else if (Time.time - lastDashTime < dashCooldown)
            {
                float remainingDashCooldown = dashCooldown - (Time.time - lastDashTime);
                dashCooldownText.text = "TP em: " + remainingDashCooldown.ToString("F1");
            }
            else
            {
                dashCooldownText.text = "TP Pronto";
            }
            
            
            if (isShieldActive)
            {
                shieldCooldownText.text = "Escudo Ativado";
            }
            else if (Time.time - lastShieldActivationTime < shieldCooldown)
            {
                float remainingShieldCooldown = shieldCooldown - (Time.time - lastShieldActivationTime);
                shieldCooldownText.text = "Escudo em: " + remainingShieldCooldown.ToString("F1");
            }
            else
            {
                shieldCooldownText.text = "Escudo Pronto";
            }
            
            float fillAmount = (float)health / maxHealth;
            healthBar.fillAmount = fillAmount;*/
            
        }
        
        
        void ToggleShield()
        {
            if (stage3 == true)
            {
                float currentTime = Time.time;
    
                if (!isShieldActive && (currentTime - lastShieldActivationTime >= shieldCooldown))
                {
                    
                    shieldObject = new GameObject("Shield");
                    SpriteRenderer spriteRenderer = shieldObject.AddComponent<SpriteRenderer>();
                    spriteRenderer.sprite = shieldSprite;
                    spriteRenderer.sortingOrder = 3;
    
                    
                    Vector3 shieldPosition = transform.position;
                    shieldPosition.y -= 0.2f; 
                    shieldObject.transform.position = shieldPosition;
    
                    shieldObject.transform.parent = transform;
                    isShieldActive = true;
                    
                    StartCoroutine(DeactivateShield());
                    
                    lastShieldActivationTime = currentTime;
                }
                else
                {
                    Destroy(shieldObject);
                    isShieldActive = false;
                }
            }
    
            IEnumerator DeactivateShield()
            {
                yield return new WaitForSeconds(shieldDuration);
                Destroy(shieldObject);
                isShieldActive = false;
            }
        }
    
        private void FixedUpdate()
        {
            Move();
        }
    
        /*void Dash()
        {      
            if (!canDash)
            {
                return; 
            }
            if (isDashActive || Time.time - lastDashTime < dashCooldown)
            {
                return; 
            }
    
            if (stage2 == true)
            {
                if (Input.GetKeyDown(KeyCode.V)) 
                {
                    PerformDash(Vector2.right);
                }
                else if (Input.GetKeyDown(KeyCode.Z)) 
                {
                    PerformDash(Vector2.left);
                }
                else if (Input.GetKeyDown(KeyCode.X)) 
                {
                    PerformDash(Vector2.up);
                }
                else if (Input.GetKeyDown(KeyCode.C)) 
                {
                    PerformDash(Vector2.down);
                }
            }
            
        }
        
        void PerformDash(Vector2 dashDirection)
        {      
            
            dashParticles.Play();
    
            if (isDashActive || Time.time - lastDashTime < dashCooldown)
            {
                return; 
            }
    
            isDashing = true;
            isInvulnerable = true;
            isDashActive = true;
    
            Vector2 dashPosition = (Vector2)transform.position + dashDirection.normalized * dashDistance;
            
            RIG.MovePosition(dashPosition);
            StartCoroutine(StopDash());     
            
            lastDashTime = Time.time; 
        }
        
        IEnumerator StopDash()
        {
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;
            isInvulnerable = false;
            isDashActive = false;
            
            dashParticles.Stop();
    
            canDash = false; 
    
            yield return new WaitForSeconds(dashCooldown); 
    
            canDash = true; 
        }*/
        
        void Move()
        {
            M = Input.GetAxis("Horizontal");
            if (!isDashing) 
            {
                RIG.velocity = new Vector2(M * speed, RIG.velocity.y);
                if (M > 0)
                {
                    if (!IsJumPing)
                    {
                        AN.SetInteger("transition", 1);
                    }
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                if (M < 0)
                {
                    if (!IsJumPing)
                    {
                        AN.SetInteger("transition", 1);
                    }
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                if (M == 0 && !IsJumPing && !isfire)
                {
                    AN.SetInteger("transition", 0);
                }
            }
        }
        void Jump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (!IsJumPing)
                {
                    AN.SetInteger("transition", 2);
                    RIG.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    //DoubleJump = true;
                    IsJumPing = true;
                    //SomPulo.Play();
                }
                /*else
                {
                    if (DoubleJump)
                    {
                        AN.SetInteger("transition", 2);
                        RIG.AddForce(new Vector2(0, jumpForce * 1), ForceMode2D.Impulse);
                        DoubleJump = false;
                        SomPulo.Play();
                    }
                }*/
            }
        }
    
        void AT()
        {
            StartCoroutine("ATA");
        }
    
        IEnumerator ATA()
        {
            if (canFire && Input.GetKeyDown(KeyCode.B))
            {
                canFire = false;
                isfire = true;
                AN.SetInteger("transition", 3);
                
                yield return new WaitForSeconds(0.9f);
                isfire = false;
                AN.SetInteger("transition", 0);
                
                //yield return new WaitForSeconds(0.3f);
                canFire = true;
            }
        }
    
        public void Damage(int DM)
        {
            if (!isShieldActive && !isInvulnerable) // Verifique se o jogador não está invulnerável
            {
                health -= DM;
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
                    //GameController.instance.GameOver();
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    SomP.Stop();
                    
                }
                
                    
            }
        }


        private void OnCollisionEnter2D(Collision2D CL)
        {
            if (CL.gameObject.layer == 6)
            {
                IsJumPing = false;
            }
        }
}
