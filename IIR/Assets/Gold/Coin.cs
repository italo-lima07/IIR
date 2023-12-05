using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue;
    private AudioSource sound;


    private void Update()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D CO)
    {
        if (CO.gameObject.tag == "Player")
        {
            sound.Play();
            //gamecontroller.instance.UpdateScore(scoreValue);
            Destroy(gameObject, 0.1f);
        }
    }
}
