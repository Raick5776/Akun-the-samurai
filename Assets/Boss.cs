using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public string playerTag = "Player"; // Tag del player
    public bool isFlipped = false;

    private Transform player; // Riferimento al player
    private BossWeapon bossWeapon; // Riferimento allo script BossWeapon

    void Start()
    {
        FindPlayer();

        // Trova il componente BossWeapon nell'arma del boss
        bossWeapon = GetComponentInChildren<BossWeapon>();
    }

    void Update()
    {
        if (player != null && player.GetComponent<PlayerHealth>().IsAlive())
        {
            LookAtPlayer();

            // Implementa qui la logica per far attaccare o spingere il boss
            // Puoi usare player.transform.position per ottenere la posizione del player

            // Chiamiamo la funzione Attack() dell'arma del boss per eseguire l'attacco
            bossWeapon.Attack();
        }
        else
        {
            FindPlayer(); // Trova il player dinamicamente se è stato rimosso dalla scena
        }
    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    public void LookAtPlayer()
    {
        if (player != null)
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }
    }
}