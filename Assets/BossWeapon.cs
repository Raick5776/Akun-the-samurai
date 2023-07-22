using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;

    public Vector3 attackOffset;
    public float attackRange = 2f;
    public LayerMask attackMask;

    public float attackCooldown = 1f; // Tempo di attesa tra un attacco e l'altro
    private bool canAttack = true; // Flag per controllare se il boss può attaccare

    public void Attack()
    {
        if (canAttack)
        {
            Debug.Log("ciao");
            Vector3 pos = transform.position;
            pos += transform.right * attackOffset.x;
            pos += transform.up * attackOffset.y;

            Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
            if (colInfo != null)
            {
                colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }

            // Imposta un ritardo prima di consentire un nuovo attacco
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false; // Imposta il flag su false per evitare attacchi ripetuti

        // Attendi per il tempo specificato nel cooldown
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true; // Consentire un nuovo attacco dopo il cooldown
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}