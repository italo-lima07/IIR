using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    private Rigidbody2D RIG;
    private void Start()
    {
    
        RIG = GetComponent<Rigidbody2D>();

    }

    public void Damage(int D)
    {
        
        health -= D;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "Player")
        {
            co.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
}
