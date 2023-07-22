using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackrate = 2f;
    float nextAttackTime = 0f;

    // Riferimenti agli AudioSource dell'attacco del player
    public AudioSource attackAudioSource;
    public AudioSource attackHitAudioSource;

    private void Start()
    {
        // Trova gli AudioSource dell'attacco del player
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            attackAudioSource = audioSources[0];
            attackHitAudioSource = audioSources[1];
        }
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackrate;
            }
        }
    }

    void Attack()
    {
        // Animazione dell'attacco
        animator.SetTrigger("Attack");

        // Avvia il sound effect dell'attacco generale
        if (attackAudioSource != null)
        {
            attackAudioSource.Play(); // Parte il sound effect dell'attacco generale
        }

        // Rilevazione dei nemici nel range dell'attacco
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Fare il danno
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hittato");
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(attackDamage);

                // Avvia il sound effect dell'attacco che colpisce il nemico
                if (attackHitAudioSource != null)
                {
                    attackHitAudioSource.Play(); // Parte il sound effect dell'attacco che colpisce il nemico
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void attacco()
    {
        if (Time.time >= nextAttackTime)
        {
            
          Attack();
                nextAttackTime = Time.time + 1f / attackrate;
           
        }
    }

}