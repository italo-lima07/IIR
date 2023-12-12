using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue;
    public AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D CO)
    {
        if (CO.gameObject.CompareTag("Player"))
        {
            sound.Play();
            gamecontroller.instance.UpdateScore(scoreValue);
            Destroy(gameObject, 0.2f);
        }
    }
}
