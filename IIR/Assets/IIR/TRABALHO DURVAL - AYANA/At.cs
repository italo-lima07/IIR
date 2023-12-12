using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class At : MonoBehaviour
{
    public int damageAmount = 10; // Quantidade de dano causado pelo ataque

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Verifica se o que foi atingido é um inimigo
        {
            other.GetComponent<Enemy>().Damage(damageAmount); // Chama a função TakeDamage do inimigo
        }
    }
}
