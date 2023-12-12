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
            if (!isDashing)
            {
                if (!isfire)
                {
                    AT();
                }

                Jump();
                Move();
            }
            
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
        
        
        void Move()
        {
            M = Input.GetAxis("Horizontal");
            if (!isDashing) 
            {
                if (M != 0 && !IsJumPing) 
                {
                    if (!SomR.isPlaying) 
                    {
                        SomR.Play();
                    }
                } 
                else 
                {
                    SomR.Stop();
                    AN.SetInteger("transition", 0); // Define a animação padrão quando não está se movendo
                }

                RIG.velocity = new Vector2(M * speed, RIG.velocity.y);

                if (M > 0)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    AN.SetInteger("transition", 1);
                }
                else if (M < 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    AN.SetInteger("transition", 1);
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
    
        void AT()
        {
            if (Input.GetKeyDown(KeyCode.B) && !isfire)
            {
                StartCoroutine(AttackAnimation());
            }
        }

        IEnumerator AttackAnimation()
        {
            isfire = true;
            AN.SetInteger("transition", 3);

            yield return new WaitForSeconds(0.1f);

            SomAtack.Play();

            yield return new WaitForSeconds(0.2f); // Tempo da animação de ataque

            isfire = false;
            AN.SetInteger("transition", 0); // Define a animação padrão
        }

        public void Damage(int DM)
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
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    SomP.Stop();
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
