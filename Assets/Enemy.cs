using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;

    private GameObject player; // Riferimento al player

    void Start()
    {
        currentHealth = maxHealth;

        // Trova il player dinamicamente
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Controlla se il player è ancora vivo prima di attaccare o spingere
        if (player != null)
        {
            // Implementa qui la logica per far attaccare o spingere i nemici
            // Puoi usare il player.transform.position per ottenere la posizione del player
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetBool("IsDead", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}