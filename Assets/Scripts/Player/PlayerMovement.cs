using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    [SerializeField] public AudioSource walkSoundEffect;
    private bool isMovementEnabled = true;
    private PlayerHealth playerHealth; // Riferimento allo script PlayerHealth

    void Start()
    {
        // Ottieni il riferimento allo script PlayerHealth
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
    }

    void Update()
    {
        if (!isMovementEnabled || !playerHealth.IsAlive()) return; // Se il movimento è disabilitato o il personaggio è morto, non permettere il movimento

        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //flip
        Vector3 characterScale = transform.localScale;
        if (joystick.Horizontal < 0)
        {
            characterScale.x = -3f;
        }
        if (joystick.Horizontal > 0)
        {
            characterScale.x = 3f;
        }
        transform.localScale = characterScale;
    }

    void FixedUpdate()
    {
        if (!isMovementEnabled || !playerHealth.IsAlive()) return; // Se il movimento è disabilitato o il personaggio è morto, non permettere il movimento

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        walkSoundEffect.Play();
    }
}