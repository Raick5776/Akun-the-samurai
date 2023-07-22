using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;

    public int health = 2000;
    public int currentHealth;

    public HealthBar healthBar;

    public string deathSceneName = "SchermataDiMorte";
    private bool isDead = false;
    private bool isAlive = true; // Aggiungi una variabile per controllare lo stato del personaggio

    private PlayerMovement playerMovement; // Riferimento allo script PlayerMovement
    public AudioSource damageAudioSource;

    private void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);

        // Ottieni il riferimento allo script PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Aggiungi questo metodo pubblico per consentire l'accesso a "isAlive" da altri script
    public bool IsAlive()
    {
        return isAlive;
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive) return; // Se il personaggio è morto, non subire ulteriori danni

        currentHealth -= damage;

        StartCoroutine(DamageAnimation());

        // Riproduci l'effetto sonoro del danno
        if (damageAudioSource != null)
        {
            damageAudioSource.Play();
        }

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            isAlive = false; // Imposta il personaggio come morto
            animator.SetTrigger("isDead");
            Debug.Log("Player died!");
        
            StartCoroutine(LoadDeathScene());

            // Disabilita il movimento del personaggio
            playerMovement.DisableMovement();
        }
    }

    IEnumerator LoadDeathScene()
    {
        yield return new WaitForSeconds(3f); // Attendiamo un po' prima di caricare la schermata di morte
        SceneManager.LoadScene(deathSceneName);
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }
}